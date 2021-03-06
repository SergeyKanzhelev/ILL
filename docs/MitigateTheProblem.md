#Mitigate the issue

## Disable IIS rapid fail protection so that IIS restarts the App indefinitely

See [this example](https://mseng.visualstudio.com/DefaultCollection/AppInsights/ChuckNorris/_git/SRT-APMService#path=%2FOnline%2FAppDiag%2FProduct%2FDataProcessor%2FApmDataWriterFE%2FRole%2FDeployment%2FUpdateIISConfig.ps1&version=GBmaster&_a=contents)

``` powershell
$AppCmdArgs = @('set', 'config', '-section:applicationPools', '-applicationPoolDefaults.failure.rapidFailProtection:false', '/commit:apphost')
&$AppCmdPath $AppCmdArgs
```

## Enable load balancer probe

This will allow to take and instance out of rotation if it's not responsive:

[LoadBalancerProbe Schema](https://msdn.microsoft.com/en-us/library/azure/jj151530.aspx)

