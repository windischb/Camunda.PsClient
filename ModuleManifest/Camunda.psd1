@{
    # If authoring a script module, the RootModule is the name of your .psm1 file
    RootModule = 'Camunda.psm1'

    Author = 'Bernhard Windisch'

    CompanyName = ''

    ModuleVersion = '0.4'

    # Use the New-Guid command to generate a GUID, and copy/paste into the next line
    GUID = 'e20cc0ad-5de8-414d-8dd9-531dfeb9adb2'

    Copyright = '(2018) Bernhard Windisch'

    Description = 'Powershell Client for Camunda BPM (https://camunda.org/)'

    # Minimum PowerShell version supported by this module (optional, recommended)
    PowerShellVersion = '5.1'

    # Which PowerShell Editions does this module work with? (Core, Desktop)
    #CompatiblePSEditions = @('Core','Desktop')

    # Which PowerShell functions are exported from your module? (eg. Get-CoolObject)
    #FunctionsToExport = @('')

    # Which PowerShell aliases are exported from your module? (eg. gco)
    #AliasesToExport = @('')

    # Which PowerShell variables are exported from your module? (eg. Fruits, Vegetables)
    VariablesToExport = @('')

    # PowerShell Gallery: Define your module's metadata
    PrivateData = @{
        PSData = @{
            # What keywords represent your PowerShell module? (eg. cloud, tools, framework, vendor)
            Tags = @('')

            # What software license is your code being released under? (see https://opensource.org/licenses)
            LicenseUri = ''

            # What is the URL to your project's website?
            ProjectUri = ''

            # What is the URI to a custom icon file for your project? (optional)
            IconUri = ''

            # What new features, bug fixes, or deprecated features, are part of this release?
            ReleaseNotes = @'
'@
        }
    }

    # If your module supports updateable help, what is the URI to the help archive? (optional)
    # HelpInfoURI = ''
}