# Deploy the demo application to azure


1. Right click on cloud service project and click "Package...". Select ```Cloud/Release``` configuration.
2. Open Azure powershell
3. Run command and type credentials in the opened window
 
	``` powershell
	Add-AzureAccount
	```

4. List azure subscriptions and select subscription you plan to use for this excersize:

	``` powershell
	Get-AzureSubscription
	Select-AzureSubscription -SubscriptionName "AI - SRT- Dev - 3"
	```
5. Create new storage account

	``` powershell
	New-AzureStorageAccount -StorageAccountName "illdemo" -Location "South Central US"
	```

4. Run command
  
	``` powershell
	Set-AzureSubscription -SubscriptionName "AI - SRT- Dev - 3" -CurrentStorageAccountName "illdemo"
	```
	
5. Run command
 
	``` powershell
	$serviceName = "ILL-demo"
	
	New-AzureService -ServiceName $serviceName -Location "South Central US"
	
	New-AzureDeployment -ServiceName $serviceName -Slot production -Package (Resolve-Path .\CloudServiceDefinition\bin\Release\app.publish\CloudServiceDefinition.cspkg) -Configuration (Resolve-Path .\CloudServiceDefinition\bin\Release\app.publish\ServiceConfiguration.Cloud.cscfg) -Label "automatic deployment - (Get-Date)"
	```

6. Wait till role will start
7. Run the page http://ill-demo.cloudapp.net
8. See telemetry in Application Insights. Notice new roleInstance in servers page.
