function Test-ModulesPath {
    [string]
    $modulesPath

    if (Test-Path($modulesPath)) {

        $fullModulesPath = "$modulesPath\Modules"

        if ((Test-Path -Path $fullModulesPath) -eq $false) {
            New-Item -ItemType Directory -Path $fullModulesPath
        }

        return $true
    } else {
        return $false
    }
}

function Get-ModulesPath {

    $myDocuments = $([Environment]::GetFolderPath("MyDocuments"))

    $modulesPath = "$myDocuments\WindowsPowerShell"

    if (Test-ModulesPath($modulesPath)) {
        return $modulesPath + "\Modules"
    }

    $modulesPath = "$myDocuments\PowerShell"

    if (Test-ModulesPath($modulesPath)) {
        return $modulesPath + "\Modules"
    }

    Write-Host
    Write-Host "env:PSModulePath $env:PSModulePath"
    Write-Host
    Write-Host "$myDocuments directory"
    Write-Host "---------------------------"    
    Get-ChildItem $myDocuments | ForEach-Object -Process { Write-Host $_.FullName }
    Write-Host "---------------------------"    
    Write-Host

    Write-Host
    Write-Host "$myDocuments\WindowsPowerShell directory"
    Write-Host "---------------------------"    
    Get-ChildItem $myDocuments\WindowsPowerShell | ForEach-Object -Process { Write-Host $_.FullName }
    Write-Host "---------------------------"    
    Write-Host

    throw "Could not find modules path in '$myDocuments'."
}

$buildScript = $(Resolve-Path $PSScriptRoot\build.ps1)
$modulesPath = Get-ModulesPath + "\VSSetup"
$vsSetupZip = $(Resolve-Path $PSScriptRoot\VSSetup.zip)

# temporary test on myget build server to see if VSSetup needs to be installed
if ($false) {
    Write-Host "Expanding '$vsSetupZip' to '$modulesPath'..."
    Expand-Archive $vsSetupZip $modulesPath
    Write-Host
}

Write-Host "Installing VSSetup module..."
Install-Module VSSetup -Scope CurrentUser -Force
Write-Host

Write-Host "Invoking build script '$buildScript'..."
Invoke-Expression ".""$buildScript"""
Write-Host
