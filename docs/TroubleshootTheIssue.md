﻿# Troubleshoot the issue

1. Open WinDbg
2. Click `Ctrl` + `D` to open crash dump
3. Type `!analyze -v` to get exception analysis

  ```
  ExceptionCode: c0000005 (Access violation)
  DemoApplication!Unknown+49 [C:\ILL\DemoApplication\Models\PackagesList.cs @ 26]
  
  FAULTING_SOURCE_CODE:  
    22:             bool result = false;
    23: 
    24:             foreach (var package in this.packages)
    25:             {
>   26:                 if (package.Id == id)
    27:                 {
    28:                     result = true;
    29:                 }
    30:             }
    31: 
  ```
4. Load sos: `.load sos`
5. WHAT TO DO NEXT? `!dso` doesn't work for me as `sos` version is incorrect for some reasons... 
6. Bingo, you found the root cause!
