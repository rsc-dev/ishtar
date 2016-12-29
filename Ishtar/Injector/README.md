# Ishtar - Injector project
## About
Injector is a small tool used to inject Dlls to remote process.
It uses CreateRemoteThread (CRT) function to call LoadLibraryA in remote process.

CRT is easy to implement, but can be easily detected by remote process (see: [DLL injection](https://en.wikipedia.org/wiki/DLL_injection)).

## How it works
1. Open target process identified by PID.
2. Get address of LoadLibraryA function from kernel32.dll. 
Note: Kernel32.dll should be loaded under constant address for each processes.
3. Allocate memory for DLL name in target process.
4. Copy DLL name to allocated memory.
5. Create remote thread in target process with LoadLibraryA address as function address and DLL name address as argument.
6. Wait for remote thread to complete.
7. Get remote thread exit code.
8. Free memory allocated in step 3.
9. Close remote process handler.

## Usage
```batchfile
c:\ishtar>Injector.exe
[-] Invalid number of arguments

Usage: injector.exe <pid> <dll>

Arguments:
 pid -- Target process ID
 dll -- Dll to load to target process
```

### Example
```batchfile
c:\ishtar>Injector.exe 1337 c:\ishtar\InjectDLL_32.dll
[+] Process token opened
[+] LUID retrieved
[+] Token privileges adjusted
[+] Dll name: c:\ishtar\InjectDLL_32.dll
[+] Remote process opened
[+] LoadLibraryA address found:76D88500
[+] Memory allocated in remote process for DLL name
[+] DLL name written to remote process memory
[+] Remote thread created
[+] Waiting for remote thread to complete
[+] Remote thread completed
[+] Remote thread exit code: 65280000
[+] Remote process memory released
[+] Remote process handle closed
[+] That's all :)
```

## License
Code is released under [MIT license](https://github.com/rsc-dev/ishtar/blob/master/LICENSE) © [Radoslaw '[rsc]' Matusiak](https://rm2084.blogspot.com/).