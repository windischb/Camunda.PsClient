# Camunda.PsClient
Camunda Client for PowerShell Core

# Usage

`Install-Module -Name Camunda.PsClient`


## Covered API (https://docs.camunda.org/manual/7.8/reference/rest/)

### [Deployment](https://docs.camunda.org/manual/7.8/reference/rest/deployment/)
|Command              |Status   |PsCommand|
|:---                 |:---:|:---|
|Get List             |:heavy_check_mark:|`Get-CamundaDeployment`|
|Get List Count       |:heavy_check_mark:|`Get-CamundaDeployment -Count`|
|Get                  |:heavy_check_mark:|`Get-CamundaDeployment`|
|Create               |:heavy_check_mark:|`New-CamundaDeployment`|
|Redeploy             |:white_circle:|:white_circle:|
|Get Resources        |:heavy_check_mark:|`Get-CamundaResource`|
|Get Resource         |:heavy_check_mark:|`Get-CamundaResource`|
|Get Resource (Binary)|:heavy_check_mark:|`Get-CamundaResourceBinary`|
|Delete               |:heavy_check_mark:|`Remove-CamundaDeployment`|

### [External Task](https://docs.camunda.org/manual/7.8/reference/rest/external-task/)

|Command              |Status   |PsCommand|
|:---                 |:---:|:---|
|Get       |:heavy_check_mark:|`Get-CamundaExternalTask`|
|Get List (POST)       |:heavy_check_mark:|`Get-CamundaExternalTask`|
|Get List Count (POST)      |:heavy_check_mark:|`Get-CamundaExternalTask -Count`|
|Fetch And Lock      |:heavy_check_mark:|`Lock-CamundaExternalTask`|
|Complete      |:heavy_check_mark:|`Complete-CamundaExternalTask`|
|Handle BPMN Error      |:heavy_check_mark:|`Send-CamundaExternalTaskError`|
|Handle Failure      |:heavy_check_mark:|`Send-CamundaExternalTaskFailure`|
|Unlock      |:heavy_check_mark:|`Unlock-CamundaExternalTask`|

### [Process Definition](https://docs.camunda.org/manual/7.8/reference/rest/process-definition/)

|Command              |Status   |PsCommand|
|:---                 |:---:|:---|
|Start Instance       |:heavy_check_mark:|`Start-CamundaProcess`|

### [Task](https://docs.camunda.org/manual/7.8/reference/rest/task/)

|Command              |Status   |PsCommand|
|:---                 |:---:|:---|
|Get       |:heavy_check_mark:|`Get-CamundaTask`|
|Get List (POST)       |:heavy_check_mark:|`Get-CamundaTask`|
|Get List Count (POST)      |:heavy_check_mark:|`Get-CamundaTask -Count`|
|Complete      |:heavy_check_mark:|`Complete-CamundaTask`|