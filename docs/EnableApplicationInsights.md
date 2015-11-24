# Enable Application Insights for the demo application

In this step you will enable Application Insights for the demo applicaiton. 

1. Clone [repository](https://github.com/SergeyKanzhelev/ILL)

	``` bash
	cd c:\
	git clone https://github.com/SergeyKanzhelev/ILL.git
	```
2. Open ```c:\ill\ILL.sln``` in Visual Studio 2015.
3. Make sure that application compiles and run when you press ```F5```.
4. Open ```Package Manager Console```. Type

	``` powershell
	Install-Package Microsoft.ApplicationInsights.Web -Pre
	```
	
5. Run application by pressing ```F5```. You'll see the message in Debug Output window:

	```
	Application Insights instrumentation key was not provided. 
	Please set instrumentation key either in the ApplicationInsights.config or programmatically.
	```

6. Open Azure portal by typing http://portal.azure.com in browser
7. Create new Application Insights application, take it's instrumentation key
8. Open ```Global.asax``` file. Add the following instrumentaiton key initialization:

	``` csharp
	try
	{
		TelemetryConfiguration.Active.InstrumentationKey = "0a851b34-2593-4151-a7de-350f2871be5b"; 
	}
	catch (Exception)
	{
		// It seems that the app is not running from emulator or in the cloud. No telemetry wll be send
	}
	```

9. In cloud service project double click on ```DemoApplication``` role name node.
10. Tick the checkbox *Enable diagnostics* and then *Send diagnostics data to Application Insights*. In opened window select *Manually specified Application Insights instumentation key* and paste instrumentaiton key.
11. Open `Global.asax` file. Add the following instrumentaiton key initialization and resolve missing `using` statements:

	``` csharp
	try
	{
		TelemetryConfiguration.Active.InstrumentationKey =
		    RoleEnvironment.GetConfigurationSettingValue("APPINSIGHTS_INSTRUMENTATIONKEY");
	}
	catch (Exception)
	{
		TelemetryConfiguration.Active.InstrumentationKey = "0a851b34-2593-4151-a7de-350f2871be5b";
	}
	```

12. Run application again. Now you'll see Application Insights telemetry in the portal.

##Additional links

- Enable [Application Insights for cloud services](https://azure.microsoft.com/documentation/articles/app-insights-cloudservices/)
- Azure diagnostics [integration with Application Insights](https://azure.microsoft.com/blog/azure-diagnostics-integration-with-application-insights/)
