$buildScript = $(Resolve-Path $PSScriptRoot\build.ps1)
$modulesPath = $(Resolve-Path "$([Environment]::GetFolderPath("MyDocuments"))\WindowsPowerShell\Modules\VSSetup")
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
