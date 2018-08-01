



Remove-Item -Path .\ModuleBuild\* -Force -Recurse
dotnet build .\Camunda.PsClient\Camunda.PsClient.csproj -r release -o ..\ModuleBuild\Camunda\core -f netstandard2.0
dotnet build .\Camunda.PsClient\Camunda.PsClient.csproj -r release -o ..\ModuleBuild\Camunda\full -f net461
Copy-Item -Path .\ModuleManifest\*.* -Destination .\ModuleBuild\Camunda\

