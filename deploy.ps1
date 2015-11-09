$serviceName = "MusicStore"
$serviceLocation = "South Central US"

$service = Get-AzureService -ServiceName $serviceName -ErrorVariable errPrimaryService -Verbose:$false -ErrorAction "SilentlyContinue"

if ($service -eq $null)
{
    New-AzureService -ServiceName $serviceName -Location $serviceLocation -ErrorVariable errPrimaryService -Verbose:$false 
}

New-AzureDeployment -ServiceName $serviceName -Slot production -Package (Resolve-Path .\AzureCloudService\bin\Release\app.publish\AzureCloudService.cspkg) -Configuration (Resolve-Path .\AzureCloudService\bin\Release\app.publish\ServiceConfiguration.Cloud.cscfg) -Label "automatic deployment - (Get-Date)"