//
// Simple CreateRemoteThread DLL injector.
//
// @author Radoslaw Matusiak
#include "stdafx.h"


LPCTSTR INJECT_DLL = _TEXT("InjectDLL.dll");
TCHAR INJECT_DLL_PATH[MAX_PATH];

// Command line arguments structure
typedef struct args {
	int pid;
	std::wstring assemblyPath;
	std::wstring typeName;
	std::wstring methodName;
	std::wstring argument;

	// Return combined string
	// @return: String <assemblyPath>;<typeName>;<methodName>;<argument>
	std::wstring combined()
	{
		static std::wstring delim(L";");

		return assemblyPath + delim + typeName + delim + methodName + delim + argument;
	}

} Args;


// Enable debug privilege for current process.
//
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


// Fill InjectDLL path.
//
// @return Returns 0 if succeeded, error code otherwise.
int FillInjectDllPath()
{
	TCHAR current_dir[MAX_PATH];
	DWORD ret = GetCurrentDirectory(MAX_PATH, current_dir);
	if (0 == ret)
	{
		std::cout << "[-] Could not get current directory" << std::endl;
		return -1;
	}

	size_t size = ret + wcslen(INJECT_DLL);
	if (MAX_PATH < size)
	{
		std::cout << "[-] Path exceeds MAX_PATH" << std::endl;
		return -1;
	}

	LPTSTR path = PathCombine(INJECT_DLL_PATH, current_dir, INJECT_DLL);
	if (NULL == path)
	{
		std::cout << "[-] Could not create path for InjectDLL" << std::endl;
		return -1;
	}

	return 0;
}

// Calculate length of wstring in bytes including null termination char.
inline size_t GetStringBytes(const std::wstring& str)
{
	return ((str.size() + 1) * sizeof(wchar_t));
}

// Run remote thread.
//
// @param process -- Remote process handler.
// @param function -- Remote process function pointer.
// @param argument -- Function argument.
// @return Remote thread exit code.
DWORD RunRemoteThread(const HANDLE process, const LPVOID function, const std::wstring& argument)
{
	// Allocate memory for function arguments in target process.
	LPVOID mem = VirtualAllocEx(process, NULL, GetStringBytes(argument), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
	if (!mem)
	{
		std::cout << "[-] Could not allocate memory" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Memory allocated in remote process " << std::endl;

	// Copy DLL name to allocated memory.	
	BOOL succeeded = WriteProcessMemory(process, mem, argument.c_str(), GetStringBytes(argument), NULL);
	if (!succeeded)
	{
		std::cout << "[-] Could not write remote process memory" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Argument written to remote process memory" << std::endl;

	// Create remote thread in target process for a given function(argument).
	HANDLE remoteThread = CreateRemoteThread(process, NULL, 0, (LPTHREAD_START_ROUTINE)function, mem, NULL, 0);
	if (!remoteThread)
	{
		DWORD errNo = GetLastError();
		std::cout << "[-] Could not create remote thread. Error code: " << errNo << std::endl;
		return errNo;
	}
	std::cout << "[+] Remote thread created" << std::endl;

	// Wait for remote thread to complete.
	std::cout << "[+] Waiting for remote thread to complete" << std::endl;
	DWORD status = WaitForSingleObject(remoteThread, INFINITE);
	std::cout << "[+] Remote thread completed" << std::endl;

	// Free allocated memory.
	succeeded = VirtualFreeEx(process, mem, 0, MEM_RELEASE);
	if (!succeeded)
	{
		std::cout << "[-] Could not release remote process memory" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Remote process memory released" << std::endl;

	// Get remote thread exit code.
	DWORD exitCode = 0;
	succeeded = GetExitCodeThread(remoteThread, &exitCode);
	if (!succeeded)
	{
		std::cout << "[-] Could not get remote thread exit code" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Remote thread exit code: " << exitCode << std::endl;

	// Return the exit code of remote thread.
	return exitCode;
}

// Returns remote process module handler.
//
// @param pid -- Remote process identifier.
// @param functionName -- Module name.
// @return Remote process module handler if found, 0 otherwise.
DWORD_PTR GetRemoteModuleHandle(const int pid, LPCTSTR moduleName)
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
//
// @param library -- Library path.
// @param functionName -- Function name.
// @return Function offset.
DWORD_PTR GetFunctionOffset(LPCTSTR library, LPCSTR functionName)
{
	HMODULE libHandler = LoadLibrary(library);
	
	void* functionAddress = GetProcAddress(libHandler, functionName);
	DWORD_PTR functionOffset = (DWORD_PTR)functionAddress - (DWORD_PTR)libHandler;

	FreeLibrary(libHandler);

	return functionOffset;
}

// Run managed code in remote process.
// Workflow:
// 1. Inject InjectDLL.dll to remote process
// 2. Use LoadManagedCode to load managed assembly in remote process
// 3. Unload InjectDLL.dll
//
// Arguments:
// @param args -- Args structure.
//
// @return Returns 0 if succeeded, error code otherwise.
DWORD Inject(Args &args)
{
	BOOL succeeded = false;

	// Open target process identified by PID.
	HANDLE process = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_READ |
		PROCESS_VM_WRITE | PROCESS_VM_OPERATION, FALSE, args.pid);
	if (!process)
	{
		std::cout << "[-] Could not open process" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] Remote process opened" << std::endl;

	// Get address of LoadLibraryW function from kernel32.dll.
	// Short: Kernel32.dll should be loaded under constant address for each processes.
	LPVOID loadLibrary = (LPVOID)GetProcAddress(GetModuleHandle(L"kernel32.dll"), "LoadLibraryW");
	if (!loadLibrary)
	{
		std::cout << "[-] Could not find address of LoadLibraryW" << std::endl;
		return GetLastError();
	}
	std::cout << "[+] LoadLibraryW address found:" << std::hex << loadLibrary << std::endl;

	// Get InjectDLL.dll path. Assums it is in current directory.
	int ret = FillInjectDllPath();
	if (FAILED(ret))
	{
		return ret;
	}

	// Load InjectDLL.dll to remote process.
	std::cout << "[+] Remote thread: LoadLibrary" << std::endl;
	RunRemoteThread(process, loadLibrary, INJECT_DLL_PATH);

	// Calculate remote InjectDll.dll LoadManagedCode function offset.
	DWORD_PTR remoteInjectDll = GetRemoteModuleHandle(args.pid, INJECT_DLL);
	DWORD_PTR offset = GetFunctionOffset(INJECT_DLL_PATH, "LoadManagedCode");
	DWORD_PTR loadManagedCode = remoteInjectDll + offset;

	std::wstring argument = args.combined();

	// Load managed code in remote thread and execute it.
	std::cout << "[+] Remote thread: LoadManagedCode" << std::endl;
	RunRemoteThread(process, (LPVOID)loadManagedCode, argument);

	// Unload InjectDLL.dll in remote process.
	LPVOID freeLibrary = (LPVOID)GetProcAddress(GetModuleHandle(L"kernel32.dll"), "FreeLibrary");
	CreateRemoteThread(process, NULL, 0, (LPTHREAD_START_ROUTINE)freeLibrary, (LPVOID)remoteInjectDll, NULL, 0);

	// Close remote process handler and exit.
	CloseHandle(process);
	std::cout << "[+] That's all :)" << std::endl;
	return 0;
}

// Print usage info.
void Usage()
{
	std::cout << "Usage: injector.exe <pid> <assemblyPath> <typeName> <methodName> [argument]" << std::endl;
	std::cout << std::endl;
	std::cout << "Arguments:" << std::endl;
	std::cout << " pid -- Target process ID" << std::endl;
	std::cout << " assemblyPath -- Path to assembly to load." << std::endl;
	std::cout << " typeName -- Type name." << std::endl;
	std::cout << " methodName -- Method name to invoke." << std::endl;
	std::cout << " argument -- Optional string argument to <method>" << std::endl << std::endl;
	
	std::cout << "Example: injector.exe 1337 c:\\inject\\ManagedLoader.exe ManagedLoader.Program Load" << std::endl;
}

// Entry point.
int _tmain(int argc, wchar_t* argv[])
{
	Args args;

	if (argc < 5)
	{
		std::cout << "[-] Invalid number of arguments" << std::endl;
		Usage();
		return -1;
	}

	args.pid = _tstoi(argv[1]);
	args.assemblyPath = argv[2];
	args.typeName = argv[3];
	args.methodName = argv[4];
	args.argument = argc == 6 ? argv[5] : L"";

	DWORD_PTR remoteModule = NULL;
	DWORD_PTR functionOffset = NULL;

	DWORD errNo = EnableDebugPrivilege();
	if (!errNo) errNo = Inject(args);

	if (errNo) std::cout << "[-] Last error code: " << std::hex << errNo << std::endl;

	return 0;
}

