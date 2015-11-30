# Advanced Azure Diagnostics with Application Insights

## Introduction
Welcome to **Advanced Azure Diagnostics with Application Insights**!

One of the top issues in Azure Cloud Services is detecting and diagnosing sporadic role failures. In this lab we will harness the power of Application Insights in tandem with the native debuggers to detect and diagnose a sporadic and pesky problem that causes a cloud service to randomly fail. We will dig deep into the detection and diagnostics capabilities of Application Insights and how it enables you to quickly root cause the problem.

## Before you start

You should be familiar with Azure Cloud Services and should have basic debugging skills.

### Prerequisites
- Visual Studio 2015
- Git tools
- Azure powershell
- Azure SDK 2.8
- Azure account
- WinDbg

## Steps

1. [Enable Application Insights for the demo application](docs/EnableApplicationInsights.md)
2. [Deploy the demo application to Azure](docs/DeployToAzure.md)
3. [Get crash dump](docs/EnableWAD.md)
4. [Troubleshoot the issue](docs/TroubleshootTheIssue.md)
5. [Mitigate the problem](docs/MitigateTheProblem.md)
