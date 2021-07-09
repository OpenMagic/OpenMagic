# None of the parameters are required. However I use them to emphasis what is unique to this script.
param (
    [ValidateNotNullOrEmpty()]
    [string] $SolutionName = "OpenMagic"
)

Write-Host "Setting PSRepository..."
Get-PSRepository
Set-PSRepository -Name PSGallery -InstallationPolicy Trusted -Verbose

Write-Host "Installing NuGet package provider..."
$null = Install-PackageProvider -Name NuGet -MinimumVersion 2.8.5.201 -Force -Scope CurrentUser

Write-Host "Installing VSSetup module..."
Install-Module VSSetup -Scope CurrentUser

# The rest of this script is fairly generic.

$ErrorActionPreference = "Stop"
$WarningPreference = "SilentlyContinue"
$VerbosePreference = "SilentlyContinue"

# Resolve-Path is used to set the following variables so test the files exist.
$solutionFolder = $(Resolve-Path $PSScriptRoot\..\)
$sln = $(Resolve-Path $solutionFolder\$solutionName.sln)
$commonBuildTasks = $(Resolve-Path $solutionFolder\submodules\common-code\source\build\Common-Build-Tasks.psm1)
$buildTasks = $(Resolve-Path $solutionFolder\build\build-tasks.ps1)

$psakeModule = "$solutionFolder\packages\psake\tools\psake\psake.psm1"
$packages = "$solutionFolder\packages"
$nuGet = "$packages\NuGet.exe"

$successful = $false

try
{    
    Import-Module $commonBuildTasks -Force
    
    Create-PackagesFolder -packages $packages
    Install-NuGet -nuGet $nuGet
    
    Install-NuGet-Package -solutionFolder $solutionFolder -packageId "psake" -excludeVersion $true
    
    Write-Host

    $properties = @{
        "solutionFolder" = $solutionFolder
        "sln" = $sln
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
