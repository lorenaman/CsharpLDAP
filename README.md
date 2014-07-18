## Novell.Directory.LDAP

This is a GitHub mirror of the `Novell.Directory.LDAP` library which is primarily hosted by Novell directly (original [here](http://www.novell.com/developer/ndk/ldap_libraries_for_c_sharp.html)).

This mirror hosts the sources for v2.1.11 for convenience, since it is otherwise necessary to use FTP to obtain the source code.

The files have been heavily modified to be compiled into a Visual Studio 2010 project.  The build files still should work.

`Mono.Security.dll` is not included in this repository. It is a required assembly when running on .NET on Windows.

Original License included.

### Building (Windows/.NET)

From a Developer Command Prompt:

```
> build.bat
```

### Building (Linux/Mono)

From a shell with mono available on `PATH`:

```
$ build.sh
```


### Visual Studio Solution

Visual Studio 2010 is supported by default, but future versions should require minimal changes.
