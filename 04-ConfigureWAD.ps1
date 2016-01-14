$currentDir = (Resolve-Path .)
. "$currentDir\00-Constants.ps1"

$configPath = ((Resolve-Path .\).Path) + "\diagnostics.wadcfgx"

$key = (Get-AzureStorageKey -StorageAccountName $storageName).Primary
"Storage key is $key"

$storageContext = New-AzureStorageContext –StorageAccountName $storageName –StorageAccountKey $key

"Enable WAD for the service $serviceName"
Set-AzureServiceDiagnosticsExtension -StorageContext $storageContext -DiagnosticsConfigurationPath $configPath –ServiceName $serviceName -Slot $deploymentSlot

