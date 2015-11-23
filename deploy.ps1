$serviceName = "ILL-demo"
$serviceLocation = "South Central US"

#Add-AzureAccount
#Set-AzureSubscription -SubscriptionName "AI - SRT- Dev - 3" -CurrentStorageAccountName "sergkanz"

$service = Get-AzureService -ServiceName $serviceName -ErrorVariable errPrimaryService -Verbose:$false -ErrorAction "SilentlyContinue"

if ($service -eq $null)
{
    New-AzureService -ServiceName $serviceName -Location $serviceLocation -ErrorVariable errPrimaryService -Verbose:$false 
}

New-AzureDeployment -ServiceName $serviceName -Slot production -Package (Resolve-Path .\CloudServiceDefinition\bin\Release\app.publish\CloudServiceDefinition.cspkg) -Configuration (Resolve-Path .\CloudServiceDefinition\bin\Release\app.publish\ServiceConfiguration.Cloud.cscfg) -Label "automatic deployment - (Get-Date)"