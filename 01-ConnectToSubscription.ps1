$currentDir = (Resolve-Path .)
. "$currentDir\00-Constants.ps1"

Add-AzureAccount
$subscriptions=Get-AzureSubscription
$subscriptions

if ($subscriptions.Count -eq 1) { 
	$subscriptionName = $subscriptions[0].SubscriptionName 
} else {
	$subscriptionName = Read-Host "Enter subscription name to use"
}

Select-AzureSubscription -SubscriptionName $subscriptionName

"Creating new storage account $storageName"
New-AzureStorageAccount -StorageAccountName $storageName -Location $storageLocation

Set-AzureSubscription -SubscriptionName $subscriptionName -CurrentStorageAccountName $storageName
