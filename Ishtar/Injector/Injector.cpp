//
// Simple CreateRemoteThread DLL injector.
//
// @author Radoslaw Matusiak

#include "stdafx.h"


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
DWORD Inject(int pid, _TCHAR* dllPath)
{
	BOOL succeeded = false;

	char dll[256];
	size_t convertedSize;
	wcstombs_s(&convertedSize, dll, dllPath, 256);
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
		std::cout << "[-] Invalid number of arguments" << std::endl << std::endl;
		Usage();
		return -1;
	}

	int pid = _tstoi(argv[1]);
	_TCHAR* pDll = argv[2];

	DWORD errNo = EnableDebugPrivilege();
	if (!errNo) 
	{ 
		errNo = Inject(pid, pDll);
	}
	
	if (errNo)
	{
		std::cout << "[-] Last error code: " << std::hex << errNo << std::endl;
	}

	return 0;
}

