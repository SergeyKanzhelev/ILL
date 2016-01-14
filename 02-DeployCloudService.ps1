$currentDir = (Resolve-Path .)
. "$currentDir\00-Constants.ps1"

"Creating new cloud service $serviceName"
New-AzureService -ServiceName $serviceName -Location $serviceLocation -ErrorVariable errPrimaryService

"Deploying bits to the new cloud service $serviceName"
New-AzureDeployment -ServiceName $serviceName -Slot $deploymentSlot -Package (Resolve-Path .\CloudServiceDefinition\bin\Release\app.publish\CloudServiceDefinition.cspkg) -Configuration (Resolve-Path .\CloudServiceDefinition\bin\Release\app.publish\ServiceConfiguration.Cloud.cscfg) -Label "automatic deployment - (Get-Date)"

$deployment = Get-AzureDeployment -ServiceName $serviceName -Slot $deploymentSlot 
$state = $deployment.RoleInstanceList[0].InstanceStatus

while ($state -ne "ReadyRole") {
	"Instance state is: $state wait another 10 seconds"
	Sleep -Seconds 10

	$deployment = Get-AzureDeployment -ServiceName $serviceName -Slot $deploymentSlot 
	$state = $deployment.RoleInstanceList[0].InstanceStatus
}

"Your instance is ready to use. Go to http://$serviceName.cloudapp.net"