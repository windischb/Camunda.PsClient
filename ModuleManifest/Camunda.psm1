Set-StrictMode -Version Latest

#
# Script module for module 'PSScriptAnalyzer'
#
Set-StrictMode -Version Latest

# Set up some helper variables to make it easier to work with the module
$PSModule = $ExecutionContext.SessionState.Module
$PSModuleRoot = $PSModule.ModuleBase

# Import the appropriate nested binary module based on the current PowerShell version
$binaryModuleRoot = $PSModuleRoot


if (($PSVersionTable.Keys -contains "PSEdition") -and ($PSVersionTable.PSEdition -ne 'Desktop')) {
    $binaryModuleRoot = Join-Path -Path $PSModuleRoot -ChildPath 'core'
}
else
{
    if ($PSVersionTable.PSVersion -lt [Version]'6.0') {
        $binaryModuleRoot = Join-Path -Path $PSModuleRoot -ChildPath 'full'
    } 
}


$binaryModulePath = Join-Path -Path $binaryModuleRoot -ChildPath 'Camunda.PsClient.dll'


$binaryModule = Import-Module -Name $binaryModulePath -PassThru

# When the module is unloaded, remove the nested binary module that was loaded with it
$PSModule.OnRemove = {
    Remove-Module -ModuleInfo $binaryModule
} 