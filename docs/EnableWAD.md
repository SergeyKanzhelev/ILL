﻿# Enable Azure diagnostics to the role

1. Open http://ill-demo.cloudapp.net/ and click *Search Microsoft* 5 times.
2. Notice that heartbeat process ID changes after every click.
3. After fifth time application became unavailable even though it is shown as "Running" in azure portal
4. You see plenty of `NullReferenceException` in Application Insights
5. Reboot the role
6. Open `c:\ill\diagnostics.wadcfgx`, replace `ApplicationInsights` key with the instrumentation key for your application and `StorageAccount` with your storage account.
5. Run powershell:

	``` powershell
	$storage_name = "illdemo"
	$key = "/IRtwF5boilLtf5NJ/pFHHHPu6WVo9RUIOU7spZVFIK1Izi60I7IBP1+4PmfwfO/RRcGEslpGv0Yo51WluP2Ig=="
	$deploymentSlot = "Production" 
	$serviceName = "ILL-demo"
	$configPath = ((Resolve-Path .\).Path) + "\diagnostics.wadcfgx"
	
	$storageContext = New-AzureStorageContext –StorageAccountName $storage_name –StorageAccountKey $key
	
	Set-AzureServiceDiagnosticsExtension -StorageContext $storageContext -DiagnosticsConfigurationPath $configPath –ServiceName $servicename -Slot "Production"
	``` 
2. Reproduce the failure by clicking *Search Microsoft* button again
3. See the crash dump being collected event in Application Insights
4. Open Cloud Explorer in Visual Studio 2015
5. Type the name of storage account in the search box, expand it and double click on blob ```wad-crashdumps```
6. Navigate to the path similar to this: ```WAD/654b1b0af45a44de8bd57806ca7f6447/DemoApplication/DemoApplication_IN_0/```
7. Download the crash dump
