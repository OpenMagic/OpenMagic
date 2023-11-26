# This script assumes it has been called from the solution folder
Param (
    [Parameter(Mandatory=$True)]
    [string]$majorProjectFolder,

    [Parameter(Mandatory=$True)]
    [string]$buildConfiguration
    )

if (-Not (Test-Path $majorProjectFolder)) {
    throw "majorProjectFolder '$majorProjectFolder' does not exist."
}

$copyFromFolder = "$majorProjectFolder\bin\$buildConfiguration"

if (-Not (Test-Path $copyFromFolder)) {
    throw "testsPath '$copyFromFolder' does not exist."
}

New-Item -ItemType Directory -Path .\artifacts
New-Item -ItemType Directory -Path .\artifacts\bin
Get-ChildItem -Path $copyFromFolder | Copy-Item -Destination .\artifacts\bin
