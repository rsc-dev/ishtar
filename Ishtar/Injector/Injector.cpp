//
// Simple CreateRemoteThread DLL injector.
//

#include "stdafx.h"

using namespace std;

// Enable debug privilege for current process.
// Returns 0 if succeeded, error code otherwise.
DWORD EnableDebugPrivilege()
{
	HANDLE token;
	BOOL succeeded = false;

	succeeded = OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &token);
	if (!succeeded)
	{
		cout << "[-] Could not enable debug privilege" << endl;
		return GetLastError();
	} 
	cout << "[+] Process token opened" << endl;
	
	TOKEN_PRIVILEGES privileges;
	ZeroMemory(&privileges, sizeof(privileges));
	privileges.PrivilegeCount = 1;
	privileges.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
	succeeded = LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &privileges.Privileges[0].Luid);
	if (!succeeded)
	{
		cout << "[-] Could not retrieve LUID" << endl;
		CloseHandle(token);
		return GetLastError();
	}
	cout << "[+] LUID retrieved" << endl;

	succeeded = AdjustTokenPrivileges(token, FALSE, &privileges, sizeof(privileges), NULL, NULL);
	if (!succeeded)
	{
		cout << "[-] Could not adjust token privileges" << endl;
		CloseHandle(token);
		return GetLastError();
	}
	cout << "[+] Token privileges adjusted" << endl;

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
//  pid -- Target process ID
//  dllPath -- Full path to DLL
//
// Returns 0 if succeeded, error code otherwise.
DWORD Inject(int pid, _TCHAR* dllPath)
{
	BOOL succeeded = false;

	char dll[256];
	size_t convertedSize;
	wcstombs_s(&convertedSize, dll, dllPath, 256);
	cout << "[+] Dll name: " << dll << endl;

	// 1. Open target process identified by PID
	HANDLE process = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, 
									FALSE, pid);
	if (!process)
	{
		cout << "[-] Could not open process" << endl;
		return -1;
	}
	cout << "[+] Remote process opened" << endl;

	// 2. Get address of LoadLibraryA function from kernel32.dll.
	//    Short: Kernel32.dll should be loaded under constant address for each processes.
	LPVOID loadLibrary = (LPVOID)GetProcAddress(GetModuleHandle(L"kernel32.dll"), "LoadLibraryA");
	if (!loadLibrary)
	{
		cout << "[-] Could not find address of LoadLibraryA" << endl;
		return GetLastError();
	}
	cout << "[+] LoadLibraryA address found:" << hex << loadLibrary << endl;

	// 3. Allocate memory for DLL name in target process.
	LPVOID mem = (LPVOID)VirtualAllocEx(process, NULL, strlen(dll) + 1, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
	if (!mem)
	{
		cout << "[-] Could not allocate memory" << endl;
		return GetLastError();
	}
	cout << "[+] Memory allocated in remote process for DLL name " << endl;

	// 4. Copy DLL name to allocated memory.
	succeeded = WriteProcessMemory(process, (LPVOID)mem, dll, strlen(dll) + 1, NULL);
	if (!succeeded)
	{
		cout << "[-] Could not write remote process memory" << endl;
		return GetLastError();
	}
	cout << "[+] DLL name written to remote process memory" << endl;

	// 5. Create remote thread in target process with LoadLibraryA address as function 
	//    address and DLL name address as argument.
	HANDLE remoteThread = CreateRemoteThread(process, NULL, NULL, (LPTHREAD_START_ROUTINE)loadLibrary, (LPVOID)mem, NULL, NULL);
	if (!remoteThread)
	{
		cout << "[-] Could not create remote thread" << endl;
		return GetLastError();
	}
	cout << "[+] Remote thread created" << endl;

	// 6. Wait for remote thread to complete.
	cout << "[+] Waiting for remote thread to complete" << endl;
	DWORD status = WaitForSingleObject(remoteThread, INFINITE);
	cout << "[+] Remote thread completed" << endl;

	// 7. Get remote thread exit code.
	DWORD exitCode = 0;
	succeeded = GetExitCodeThread(remoteThread, &exitCode);
	if (!succeeded)
	{
		cout << "[-] Could not get remote thread exit code" << endl;
		return GetLastError();
	}
	cout << "[+] Remote thread exit code: " << exitCode << endl;

	// 8. Free memory allocated in step 3.
	succeeded = VirtualFreeEx(process, mem, 0, MEM_RELEASE);
	if (!succeeded)
	{
		cout << "[-] Could not release remote process memory" << endl;
		return GetLastError();
	}
	cout << "[+] Remote process memory released" << endl;

	// 9. Close remote process handler.
	succeeded = CloseHandle(process);
	if (!succeeded)
	{
		cout << "[-] Could not close remote process handle" << endl;
		return GetLastError();
	}
	cout << "[+] Remote process handle closed" << endl;

	cout << "[+] That's all :)" << endl;
	return 0;
}


// Print usage info.
void Usage()
{
	cout << "Usage: injector.exe <pid> <dll>" << endl;
	cout << endl;
	cout << "Arguments:" << endl;
	cout << " pid -- Target process ID" << endl;
	cout << " dll -- Dll to load to target process" << endl;
}


// Entry point.
int _tmain(int argc, _TCHAR* argv[])
{
	if (3 != argc)
	{
		cout << "[-] Invalid number of arguments" << endl << endl;
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
		cout << "[-] Last error code: " << hex << errNo << endl;
	}

	return 0;
}

