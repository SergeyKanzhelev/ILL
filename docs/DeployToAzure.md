# Deploy the demo application to azure


1. Right click on cloud service project and click "Package...". Select ```Cloud/Release``` configuration.
2. Open Azure powershell
3. Run command and type credentials in the opened window
 
	``` powershell
	Add-AzureAccount
	```

4. Run command
  
	``` powershell
	Set-AzureSubscription -SubscriptionName "AI - SRT- Dev - 3" -CurrentStorageAccountName "sergkanz"
	```
	
5. Run command
 
	``` powershell
	$serviceName = "ILL-demo"
	$serviceLocation = "South Central US"
	$service = Get-AzureService -ServiceName $serviceName -ErrorVariable errPrimaryService -Verbose:$false -ErrorAction "SilentlyContinue"

	if ($service -eq $null)
	{
		New-AzureService -ServiceName $serviceName -Location $serviceLocation -ErrorVariable errPrimaryService -Verbose:$false 
	}
	
	New-AzureDeployment -ServiceName $serviceName -Slot production -Package (Resolve-Path .\CloudServiceWithLB\bin\Release\app.publish\CloudServiceWithLB.cspkg) -Configuration (Resolve-Path .\CloudServiceWithLB\bin\Release\app.publish\ServiceConfiguration.Cloud.cscfg) -Label "automatic deployment - (Get-Date)"
	```
	
6. See telemetry in Application Insights. Notice new roleInstance in servers page.
7. Run the page http://ill-demo.cloudapp.net/api/values?q=Microsoft
