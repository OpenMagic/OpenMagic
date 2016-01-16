# None of the parameters are required. However I use them to emphasis what is unique to this script.
param (
    [ValidateNotNullOrEmpty()]
    [string] $SolutionName = "OpenMagic",

    [ValidateNotNullOrEmpty()]
    [string] $specFlowVersion = "1.9.0"
)

# The rest of this script is fairly generic.

$ErrorActionPreference = "Stop"
$WarningPreference = "SilentlyContinue"
$VerbosePreference = "SilentlyContinue"

# Resolve-Path is used to set the following variables so test the files exist.
$solutionFolder = $(Resolve-Path $PSScriptRoot\..\)
$sln = $(Resolve-Path $solutionFolder\$solutionName.sln)
$commonBuildTasks = $(Resolve-Path $solutionFolder\submodules\common-code\source\build\Common-Build-Tasks.psm1)
$buildTasks = $(Resolve-Path $solutionFolder\build\build-tasks.ps1)

$psakeModule = "$solutionFolder\packages\psake\tools\psake.psm1"
$packages = "$solutionFolder\packages"
$nuGet = "$packages\NuGet.exe"

$successful = $false

try
{    
    Import-Module $commonBuildTasks -Force
    
    Create-PackagesFolder -packages $packages
    Install-NuGet -nuGet $nuGet
    
    . $nuGet
    
    Install-NuGet-Package -solutionFolder $solutionFolder -packageId "psake" -excludeVersion $true
    Install-NuGet-Package -solutionFolder $solutionFolder -packageId "SpecFlow" -version $specFlowVersion
    
    Write-Host

    $properties = @{
        "solutionFolder" = $solutionFolder
        "sln" = $sln
        "specFlowVersion" = $specFlowVersion
    }

    Import-Module $psakeModule -Force
    Invoke-psake $buildTasks -properties $properties

    $successful = $psake.build_success
}
Catch
{
    Write-Host $_.Exception.Message -ForegroundColor Red
}
Finally
{
    Write-Host
    Pop-Location

    If ($successful)
    { 
        Write-Host "Build was successful." -ForegroundColor Green
        exit 0
    }
    Else
    {
        Write-Host "Build failed." -ForegroundColor Red
        exit 1
    }
}
