# Ishtar - Injector project
## About
Injector is a small tool used to inject managed Dlls to remote processes and execute managed code.
It uses CreateRemoteThread (CRT) function to call LoadLibrary in remote process.

CRT is easy to implement, but can be easily detected by remote process (see: [DLL injection](https://en.wikipedia.org/wiki/DLL_injection)).

Injector consists of two parts:
1. Injector.exe - responsible for loading InjectDLL.dll to remote process and execute InjectDLL::LoadManagedCode function.
2. InjectDLL.dll - exports only one function: HRESULT LoadManagedCode(_In_ LPCTSTR arg). Once this DLL is injected to remote process, then Injector.exe calls LoadManagedCode function in order to execute managed code inside remote process.

[![Ishtar-Injector usage example](https://img.youtube.com/vi/kGNNAeGRctY/0.jpg)](https://www.youtube.com/watch?v=kGNNAeGRctY)

## How it works
1. Enable Debug privilieges for Injector process.
2. Open target process identified by PID.
2. Get address of LoadLibrary function from kernel32.dll. 
Note: Kernel32.dll should be loaded under constant address for each processes.
3. Allocate memory for DLL name in target process.
4. Copy DLL name to allocated memory.
5. Create remote thread in target process with LoadLibrary address as function address and DLL name address as argument.
6. Wait for remote thread to complete.
7. Free memory allocated in step 3.
8. Get remote process handler to injected DLL.
9. Load DLL to Injector process and calculate target function offset.
10. Allocate memory for function parameter in target process.
11. Create remote thread in target process with injected DLL function address and function parameter as argument.
12. Wait for remote thread to complete.
13. Close remote process handler.

## Usage
```batchfile
c:\ishtar>Injector.exe
[-] Invalid number of arguments
Usage: injector.exe <pid> <assemblyPath> <typeName> <methodName> [argument]

Arguments:
 pid -- Target process ID
 assemblyPath -- Path to assembly to load.
 typeName -- Type name.
 methodName -- Method name to invoke.
 argument -- Optional string argument to <method>

Example: injector.exe 1337 c:\inject\ManagedLoader.exe ManagedLoader.Program Load
```

## License
Code is released under [MIT license](https://github.com/rsc-dev/ishtar/blob/master/LICENSE) © [Radoslaw '[rsc]' Matusiak](https://rm2084.blogspot.com/).