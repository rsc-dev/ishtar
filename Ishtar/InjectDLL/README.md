# Ishtar - InjectDll project

## About
InjectDLL.dll is a native DLL exporting only one function: HRESULT LoadManagedCode(_In_ LPCTSTR arg).

LoadManagedCode function takes one string parameter. It should be in format "<assembly_path>;<type_name>;<method_name>;[parameter]", i.e.: "c:\ishtar\ManagedLoader.exe;ManagedLoader.Program;Load;".

When invoked, function enumerates running .NET runtimes in process and tries to execute ExecuteInDefaultAppDomain function with given parameters.

This library is used by [Injector.exe](https://github.com/rsc-dev/ishtar/tree/master/Ishtar/Injector).


## License
Code is released under [MIT license](https://github.com/rsc-dev/ishtar/blob/master/LICENSE) © [Radoslaw '[rsc]' Matusiak](https://rm2084.blogspot.com/).