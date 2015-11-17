#
# EnableWAD.ps1
#
$storage_name = "sergkanz"
$key = "umqulDKysRu77Wlt0ooOdsPhzY8NxNMhFsTS16WVAl75PJ+1MOlh4Nc+5tMHbxT/StR/hBMvdyY/PsydvznRKA=="
$deploymentSlot = "Production" 
$serviceName = "MusicStore"
$serviceLocation = "South Central US"
$configPath = ((Resolve-Path .\).Path) + "\crashdumps.wadcfgx"


$storageContext = New-AzureStorageContext –StorageAccountName $storage_name –StorageAccountKey $key

Set-AzureServiceDiagnosticsExtension -StorageContext $storageContext -DiagnosticsConfigurationPath  –ServiceName $servicename -Slot "Production"

# Set-AzureServiceDiagnosticsExtension -StorageContext $storageContext -DiagnosticsConfigurationPath $public_config –ServiceName $service_name -Slot ‘Production’ -Role $role_name

