Param (
    [Parameter(Mandatory=$True)]
    [string]$xunitPath,

    [Parameter(Mandatory=$True)]
    [string]$testsFolder,

    [Parameter(Mandatory=$True)]
    [string]$buildConfiguration
    )

if (-Not (Test-Path $xunitPath)) {
    throw "xunitPath '$xunitPath' does not exist."
}

if (-Not (Test-Path $testsFolder)) {
    throw "testsPath '$testsFolder' does not exist."
}

Get-ChildItem -Path $testsFolder -Directory |
        ForEach-Object {

            $fullFolder = $_.FullName
            $folderName = $_.Name
            $testAssembly = "$fullFolder\bin\$buildConfiguration\$folderName.dll"

            Write-Host "Running tests in $folderName..."
            Write-Host "----------------------------------------------------------------------"
            Write-Host

            Invoke-Expression "&""$xunitPath"" ""$testAssembly"""
            
            if ($LASTEXITCODE -ne 0) {
                throw "One or more unit tests failed in $testAssembly"
            }

            Write-Host
            Write-Host "Successfully ran all tests in $folderName."
            Write-Host
        }
