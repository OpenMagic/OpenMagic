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

Task Create-SpecFlow-Config {
    
    Create-SpecFlow-Config -specFlowToolsFolder $specFlowToolsFolder
}

Task Create-SpecFlow-Tests -depends Create-SpecFlow-Config {

    Create-SpecFlow-Tests -specFlow $specFlow -project "$solutionFolder\tests\OpenMagic.Specifications\OpenMagic.Specifications.csproj"
}

Task Compile -depends Clean, Restore-NuGet-Packages, Create-SpecFlow-Tests {

    Compile-Solution $sln $configuration
}

Task End-to-End-Tests -depends Compile {

    Run-xUnit-Tests -xunit $xunit -testsFolder $tests -configuration $configuration
}

Task Create-Bin-Artifacts {

    Create-Bin-Artifacts -bin $solutionFolder\source\$solutionName\bin\$configuration -artifacts $binArtifacts
}

Task Package -depends End-to-End-Tests, Create-Bin-Artifacts {

    Create-NuGet-Package -nuGet $nuGet -nuSpec $nuSpec -nuPkg $nuPkg -outputDirectory $nuGetArtifacts -version $nuGetVersion
    MyGet-Cleanup -packages $packages
}

FormatTaskName {
   param($taskName)

   Write-Host $taskName -ForegroundColor Yellow
   Write-Host "----------------------------------------------------------------------" -ForegroundColor Yellow
}
