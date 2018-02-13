# Camunda.PsClient
Camunda Client for PowerShell Core

## Covered API (https://docs.camunda.org/manual/7.8/reference/rest/)

### [Deployment](https://docs.camunda.org/manual/7.8/reference/rest/deployment/)
|Command              |Status   |PsCommand|
|:---                 |:---:|:---|
|Get List             |:heavy_check_mark:|`Get-CamundaDeployment`|
|Get List Count       |:heavy_check_mark:|`Get-CamundaDeployment -Count`|
|Get                  |:heavy_check_mark:|`Get-CamundaDeployment`|
|Create               |:heavy_check_mark:|`New-CamundaDeployment`|
|Redeploy             |:white_circle:|:white_circle:|
|Get Resources        |:white_circle:|:white_circle:|
|Get Resource         |:white_circle:|:white_circle:|
|Get Resource (Binary)|:white_circle:|:white_circle:|
|Delete               |:heavy_check_mark:|`Remove-CamundaDeployment`|


### [Process Definition](https://docs.camunda.org/manual/7.8/reference/rest/process-definition/)

|Command              |Status   |PsCommand|
|:---                 |:---:|:---|
|Start Instance       |:white_circle:|:white_circle:|