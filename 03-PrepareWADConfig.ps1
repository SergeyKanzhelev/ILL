$currentDir = (Resolve-Path .)
. "$currentDir\00-Constants.ps1"

$configPath = ((Resolve-Path .\).Path) + "\diagnostics.wadcfgx"

(Get-Content $configPath ).replace('YOUR-STORAGE-ACCOUNT-HERE', $storageName) | Set-Content $configPath

"Open and modify $configPath"
