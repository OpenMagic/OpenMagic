function Get-ModulesPath {

    $myDocuments = $([Environment]::GetFolderPath("MyDocuments"))

    $modulesPath = Resolve-Path "$myDocuments\WindowsPowerShell\Modules" -ErrorAction SilentlyContinue

    if ($null -ne $modulesPath) {
        return $modulesPath        
    }

    $modulesPath = Resolve-Path "$myDocuments\PowerShell\Modules" -ErrorAction SilentlyContinue

    if ($null -ne $modulesPath) {
        return $modulesPath        
    }

    Get-ChildItem $myDocuments

    throw "Could not find modules path in '$myDocuments'."
}

$buildScript = $(Resolve-Path $PSScriptRoot\build.ps1)
$modulesPath = Get-ModulesPath + "\VSSetup"
$vsSetupZip = $(Resolve-Path $PSScriptRoot\VSSetup.zip)

Write-Host "Expanding '$vsSetupZip' to '$modulesPath'..."
Expand-Archive $vsSetupZip $modulesPath
Write-Host

Write-Host "Installing VSSetup module..."
Install-Module VSSetup -Scope CurrentUser -Force
Write-Host

Write-Host "Invoking build script '$buildScript'..."
Invoke-Expression ".""$buildScript"""
Write-Host
