$solutionFolder = $(Resolve-Path $PSScriptRoot\..)
$build = $(Resolve-Path $PSScriptRoot\build.ps1)

# The following git clean will have not real effect on MyGet server.
# What is will do is make developer's directory structure same as on
# MyGet server.
#Write-Host "Running git clean..."
#&git clean -d -X --force "$solutionFolder"
#Write-Host

Write-Host "Installing VSSetup module..."
Install-Module -Name VSSetup -Scope CurrentUser -SkipPublisherCheck -Force -AllowClobber

Write-Host "Invoking build script '$build'..."
Invoke-Expression ".""$build"""
