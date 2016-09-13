# Choose-Random-Folder
---

Choose a random folder from a chosen directory, with style. Meant to be very simplistic, I created it for the purpose of randomly picking a tv series from a folder to watch in a sort of lottery style fashion, it can pick folders from any other type of directory though.

---
  
### Build & Run
**Requirements:** nuget.exe on PATH, Visual Studio 2015 and/or C# 6.0 Roslyn Compiler  
**Optional:** Devenv (Visual Studio 2015) on PATH   

```
git clone https://github.com/dukemiller/choose-random-folder.git  
cd choose-random-folder  
nuget install choose-random-folder\packages.config -OutputDirectory packages
```

**Building with Devenv (CLI):** ```devenv choose-random-folder.sln /Build```  
**Building with Visual Studio:**  Open (ctrl-shift-o) "choose-random-folder.sln", Build Solution (ctrl-shift-b)

A "choose-random-folder.exe" artifat will be created in the parent choose-random-folder folder.