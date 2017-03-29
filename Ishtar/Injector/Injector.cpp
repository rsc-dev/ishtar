//
// Simple CreateRemoteThread DLL injector.
//
// @author Radoslaw Matusiak
#include "stdafx.h"


LPCTSTR INJECT_DLL = L"InjectDLL.dll";


// Enable debug privilege for current process.
// @return Returns 0 if succeeded, error code otherwise.
DWORD EnableDebugPrivilege()
{
	HANDLE token;
	BOOL succeeded = false;

	succeeded = OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &token);
	if (!succeeded)
	{
		std::cout << "[-] Could not enable debug privilege" << std::endl;
		return GetLastError();
	} 
	std::cout << "[+] Process token opened" << std::endl;
	
	TOKEN_PRIVILEGES privileges;
	ZeroMemory(&privileges, sizeof(privileges));
	privileges.PrivilegeCount = 1;
	privileges.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
	succeeded = LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &privileges.Privileges[0].Luid);
	if (!succeeded)
	{
		std::cout << "[-] Could not retrieve LUID" << std::endl;
		CloseHandle(token);
		return GetLastError();
	}
	std::cout << "[+] LUID retrieved" << std::endl;

	succeeded = AdjustTokenPrivileges(token, FALSE, &privileges, sizeof(privileges), NULL, NULL);
	if (!succeeded)
	{
		std::cout << "[-] Could not adjust token privileges" << std::endl;
		CloseHandle(token);
		return GetLastError();
	}
	std::cout << "[+] Token privileges adjusted" << std::endl;

	CloseHandle(token);

	return 0;
}


// Inject given DLL to process.
// Injection method - CreateRemoteThread + LoadLibrary.
// Workflow:
// 1. Open target process identified by PID.
// 2. Get address of LoadLibraryA function from kernel32.dll.
//    Short: Kernel32.dll should be loaded under constant address for each processes.
// 3. Allocate memory for DLL name in target process.
// 4. Copy DLL name to allocated memory.
// 5. Create remote thread in target process with LoadLibraryA address as function 
//    address and DLL name address as argument.
// 6. Wait for remote thread to complete.
// 7. Get remote thread exit code.
// 8. Free memory allocated in step 3.
// 9. Close remote process handler.
//
// Arguments:
//  @param pid -- Target process ID
//  @param dllPath -- Full path to DLL
//
// @return Returns 0 if succeeded, error code otherwise.
DWORD Inject(const int pid, LPCTSTR  library)
{
	BOOL succeeded = false;

	char dll[256];
	size_t convertedSize;
	wcstombs_s(&convertedSize, dll, library, 256);
	std::cout << "[+] Dll name: " << dll << std::endl;

	// 1. Open target process identified by PID
	HANDLE process = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, 
									FALSE, pid);
	if (!process)
	{
		std::cout << "[-] Could not open process" << std::endl;
		return -1;
	}
	std::cout << "[+] Remote process opened" << std::endl;

	// 2. Get address of LoadLibraryA function from kernel32.dll.
	//    Short: Kernel32.dll should be loaded under constant address for each processes.
	LPVOID loadLibrary = (LPVOID)GetProcAddress(GetModuleHandle(L"kernel32.dll"), "LoadLibraryA");
	if (!loadLibrary)
	{
		std::cout << "[-] Could not find address of LoadLibraryA" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] LoadLibraryA address found:" << std::hex << loadLibrary << std::endl;

	// 3. Allocate memory for DLL name in target process.
	LPVOID mem = (LPVOID)VirtualAllocEx(process, NULL, strlen(dll) + 1, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
	if (!mem)
	{
		std::cout << "[-] Could not allocate memory" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Memory allocated in remote process for DLL name " << std::endl;

	// 4. Copy DLL name to allocated memory.
	succeeded = WriteProcessMemory(process, (LPVOID)mem, dll, strlen(dll) + 1, NULL);
	if (!succeeded)
	{
		std::cout << "[-] Could not write remote process memory" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] DLL name written to remote process memory" << std::endl;

	// 5. Create remote thread in target process with LoadLibraryA address as function 
	//    address and DLL name address as argument.
	HANDLE remoteThread = CreateRemoteThread(process, NULL, NULL, (LPTHREAD_START_ROUTINE)loadLibrary, (LPVOID)mem, NULL, NULL);
	if (!remoteThread)
	{
		std::cout << "[-] Could not create remote thread" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Remote thread created" << std::endl;

	// 6. Wait for remote thread to complete.
	std::cout << "[+] Waiting for remote thread to complete" << std::endl;
	DWORD status = WaitForSingleObject(remoteThread, INFINITE);
	std::cout << "[+] Remote thread completed" << std::endl;

	// 7. Get remote thread exit code.
	DWORD exitCode = 0;
	succeeded = GetExitCodeThread(remoteThread, &exitCode);
	if (!succeeded)
	{
		std::cout << "[-] Could not get remote thread exit code" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Remote thread exit code: " << exitCode << std::endl;

	// 8. Free memory allocated in step 3.
	succeeded = VirtualFreeEx(process, mem, 0, MEM_RELEASE);
	if (!succeeded)
	{
		std::cout << "[-] Could not release remote process memory" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Remote process memory released" << std::endl;

	// 9. Close remote process handler.
	succeeded = CloseHandle(process);
	if (!succeeded)
	{
		std::cout << "[-] Could not close remote process handle" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Remote process handle closed" << std::endl;

	std::cout << "[+] That's all :)" << std::endl;
	return 0;
}

//
// @return: Remote process module handler.
DWORD_PTR GetRemoteModuleHandle(const int pid, LPCTSTR  moduleName)
{
	MODULEENTRY32 moduleEntry;
	moduleEntry.dwSize = sizeof(MODULEENTRY32);
	
	// Takes a snapshot of the specified processes, including modules.
	HANDLE targetSnapshot = INVALID_HANDLE_VALUE;
	targetSnapshot = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE, pid);

	// Start looking for injected modules ...
	if (!Module32First(targetSnapshot, &moduleEntry))
	{
		CloseHandle(targetSnapshot);
		return 0;
	}

	// ... iterate until we find injected module (or reach the end).
	while (wcscmp(moduleEntry.szModule, moduleName) != 0 && Module32Next(targetSnapshot, &moduleEntry));

	CloseHandle(targetSnapshot);

	// Return injected module handler if it was found.
	if (wcscmp(moduleEntry.szModule, moduleName) == 0)
	{
		return (DWORD_PTR)moduleEntry.modBaseAddr;
	}

	return 0;
}

// Calculate function offset from library address.
// @param library: Library path.
// @param functionName: Function name.
// @return: Function offser.
DWORD_PTR GetFunctionOffset(LPCTSTR library, LPCTSTR functionName)
{
	// Macro required for T2A macro.
	USES_CONVERSION;

	HMODULE libHandler = LoadLibrary(library);
	
	void* functionAddress = GetProcAddress(libHandler, T2A(functionName));
	
	DWORD_PTR functionOffset = (DWORD_PTR)functionAddress - (DWORD_PTR)libHandler;

	FreeLibrary(libHandler);

	return functionOffset;
}

// Print usage info.
void Usage()
{
	std::cout << "Usage: injector.exe <pid> <dll>" << std::endl;
	std::cout << std::endl;
	std::cout << "Arguments:" << std::endl;
	std::cout << " pid -- Target process ID" << std::endl;
	std::cout << " dll -- Dll to load to target process" << std::endl;
}


// Entry point.
int _tmain(int argc, _TCHAR* argv[])
{
	if (3 != argc)
	{
		std::cout << "[-] Invalid number of arguments" << std::endl;
		Usage();
		return -1;
	}

	int pid = _tstoi(argv[1]);
	LPCTSTR library = argv[2];
	DWORD_PTR remoteModule = NULL;
	DWORD_PTR functionOffset = NULL;

	DWORD errNo = EnableDebugPrivilege();
	if (!errNo) 
	{ 
		// 1. Inject DLL
		TCHAR dir[MAX_PATH - 50];
		DWORD ret = GetCurrentDirectory(MAX_PATH - 50, dir);
		if (0 == ret)
		{
			std::cout << "[-] Could not get current directory" << std::endl;
			return -1;
		}

		TCHAR injectDllPath[MAX_PATH];
		LPTSTR path = PathCombine(injectDllPath, dir, INJECT_DLL);
		if (NULL == path)
		{
			std::cout << "[-] Could not create path for InjectDLL" << std::endl;
			return -1;
		}

		errNo = Inject(pid, injectDllPath);
		// 2. Get injected DLL remote module handler
		remoteModule = GetRemoteModuleHandle(pid, INJECT_DLL);
		// 3. Calculate offset
		functionOffset = GetFunctionOffset(injectDllPath, L"LoadManagedCode");
	}
	
	if (errNo)
	{
		std::cout << "[-] Last error code: " << std::hex << errNo << std::endl;
	}

	return 0;
}

