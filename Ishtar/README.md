# Ishtar Solution
Ishtar is a set of projects used to hack into .NET applications in runtime.

## [InjectDLL](https://github.com/rsc-dev/ishtar/tree/master/Ishtar/InjectDLL)
Dll to load Ishtar assemblies. It should be injected to remote process using Injector.

## [Injector](https://github.com/rsc-dev/ishtar/tree/master/Ishtar/Injector)
Injector is a simple console application used to inject DLLs to remote process.
It is used to inject InjectDLL, which loads Ishtar assemblies.
More to read: [Dll injection](https://en.wikipedia.org/wiki/DLL_injection)

## [ManagedLoader](https://github.com/rsc-dev/ishtar/tree/master/Ishtar/ManagedLoader)
Managed assemblies loader. Once it is loaded to target process, it can be used to load and run other .NET assemblies.

## [Ishtar](https://github.com/rsc-dev/ishtar/tree/master/Ishtar/Ishtar)
Managed applications takeover tool. It allows to hack .NET applications in runtime.

## [ObjectUtils](https://github.com/rsc-dev/ishtar/tree/master/Ishtar/ObjectUtils)
Managed library used for heap objects referencies hunting.
Used in Ishtar to restore referencies to objects allocated on managed heaps.

## [TestApp](https://github.com/rsc-dev/ishtar/tree/master/Ishtar/TestApp)
Managed test application. Used during development and in examples.