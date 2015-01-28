Properties {
    # Following properties are solution specific
    $solutionName = "OpenMagic"

    # Following properties are generic
    $solutionFolder = Resolve-Path ..\
    $sln = Resolve-Path $solutionFolder\$solutionName.sln
    $specFlowVersion = "1.9.0"
    $configuration = "Release"
    $packages = Resolve-Path $solutionFolder\packages
    $nuGet = Resolve-Path $packages\NuGet.exe
    $nuGetConfig = Resolve-Path $solutionFolder\NuGet.config
    $nuSpec = Resolve-Path $solutionFolder\build\$solutionName.nuspec
    $specFlowToolsFolder = Resolve-Path $packages\SpecFlow.$specFlowVersion\tools
    $specFlow = Resolve-Path $specFlowToolsFolder\specflow.exe
    $tests = Resolve-Path $solutionFolder\tests
    $xunit = "$packages\xunit.runners\tools\xunit.console.clr4.exe"
    $artifacts = "$solutionFolder\artifacts"
    $nuGetArtifacts = "$artifacts\NuGet"
    $binArtifacts = "$artifacts\bin"
    $nuGetVersion = Get-NuGet-Version
    $nuPkg = "$solutionName.$nuGetVersion.nupkg"
}

Task default -depends Validate-Properties, Package

Task Validate-Properties {

    Assert (Test-Path $solutionFolder) "solutionFolder '$solutionFolder' does not exist."
    Assert (Test-Path $sln) "sln '$sln' does not exist."
    Assert (-not [string]::IsNullOrWhitespace($specFlowVersion)) "specFlowVersion is required."
    Assert (-not [string]::IsNullOrWhitespace($configuration)) "configuration is required."
    Assert (Test-Path $packages) "packages '$packages' does not exist."
    Assert (Test-Path $nuGet) "nuGet '$nuGet' does not exist."
    Assert (Test-Path $specFlowToolsFolder) "specFlowToolsFolder '$specFlowToolsFolder' does not exist."
    Assert (Test-Path $specFlow) "specFlow '$specFlow' does not exist."
    Assert (Test-Path $tests) "tests '$tests' does not exist."

    Write-Host "Successfully validated properties."
    Write-Host
}

Task Clean {

    Clean-Solution $sln $configuration $artifacts
}

Task Restore-NuGet-Packages {

    Restore-NuGet-Packages -nuGet $nuGet -nuGetConfig $nuGetConfig -sln $sln -packages $packages
}

Task Generate-SpecFlow-Tests {

    Write-Host "Generating SpecFlow tests..."
    Write-Host

    $specFlowConfig = "$specFlowToolsFolder\specflow.exe.config"

    Remove-Item $specFlowConfig -ErrorAction SilentlyContinue
    Add-Content $specFlowConfig "<?xml version=""1.0"" encoding=""utf-8"" ?>"
    Add-Content $specFlowConfig "<configuration>"
    Add-Content $specFlowConfig "<startup>"
    Add-Content $specFlowConfig "<supportedRuntime version=""v4.0.30319"" />"
    Add-Content $specFlowConfig "</startup>"
    Add-Content $specFlowConfig "</configuration>"

    Exec { Invoke-Expression "&""$specFlow"" generateall ""$solutionFolder\tests\OpenMagic.Specifications\OpenMagic.Specifications.csproj"" /force" }

    Write-Host
    Write-Host "Successfully generated SpecFlow tests."
    Write-Host
}

Task Compile -depends Clean, Restore-NuGet-Packages, Generate-SpecFlow-Tests {

    Compile-Solution $sln $configuration

    Write-Host "Creating $binArtifacts folder..."
    New-Item -Path $binArtifacts -ItemType Directory | Out-Null
    Write-Host "Successfully created $binArtifacts folder."
    
    Write-Host
    Write-Host "Copying $solutionName binaries to $binArtifacts..."
    Get-ChildItem -Path $solutionFolder\source\$solutionName\bin\$configuration |
        Copy-Item -Destination $binArtifacts
    Write-Host "Successfully copied $solutionName binaries to $binArtifacts."

    Write-Host
}

Task End-to-End-Tests -depends Compile {

    Run-xUnit-Tests -xunit $xunit -testsFolder $tests -configuration $configuration
}

Task Package -depends End-to-End-Tests {

    Write-Host "Creating $nuGetArtifacts folder..."
    New-Item -Path $nuGetArtifacts -ItemType Directory | Out-Null
    Write-Host "Successfully created $nuGetArtifacts folder."

    Write-Host "Creating $nuPkg..."
    Exec { Invoke-Expression "&""$nuGet"" pack ""$nuSpec"" -OutputDirectory ""$nuGetArtifacts"" -Version $nuGetVersion" }
    Write-Host "Successfully created $nupkg."
}

FormatTaskName {
   param($taskName)

   Write-Host $taskName -ForegroundColor Yellow
   Write-Host "----------------------------------------------------------------------" -ForegroundColor Yellow
}
