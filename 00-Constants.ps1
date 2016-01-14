#Storage name used to deploy cloud service to Azure and store WAD data and crash dumps
$storageName = "illdemo" + "yournamelowercase"
$storageLocation = "South Central US"

# Cloud service name and properties
$serviceName = $storageName
$deploymentSlot = "Production" 
$serviceLocation = $storageLocation


