# Solution specific variables
$SolutionName = "OpenMagic"

$ErrorActionPreference = "Stop"
$WarningPreference = "SilentlyContinue"
$VerbosePreference = "SilentlyContinue"

Push-Location $PSScriptRoot

try
{
    Import-Module .\submodules\common-code\source\build\Common-Build-Tasks.psm1 -Force
    
    Create-PackagesFolder
    Install-NuGet
    Install-NuGet-Package -packageId "psake" -excludeVersion $true
    Install-NuGet-Package -packageId "SpecFlow"
    
    Write-Host
    .\packages\psake\tools\psake.ps1 .\build-tasks.ps1
}
Finally
{
    Pop-Location
}

If ($psake.build_success -eq $false)
{ 
    exit 1
}
Else
{
    exit 0
}
