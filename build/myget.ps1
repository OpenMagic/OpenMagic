$solutionFolder = $(Resolve-Path $PSScriptRoot\..)
$build = $(Resolve-Path $PSScriptRoot\build.ps1)

# The following git clean will have not real effect on MyGet server.
# What is will do is make developer's directory structure same as on
# MyGet server.
&git clean -d -X --force "$solutionFolder"
Write-Host

Invoke-Expression ".""$build"""
