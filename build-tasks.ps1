Properties {
    $sln = Resolve-Path OpenMagic.sln
    $configuration = "Release"
}

Task default -depends end-to-end-tests

Task Clean {

    Clean-Solution $sln $configuration
}

Task Restore-NuGet-Packages {

    Restore-NuGet-Packages $sln
}

Task Generate-SpecFlow-Tests {

    Write-Host "Generating SpecFlow tests..."
    Write-Host

    Remove-Item .\packages\SpecFlow.1.9.0\tools\specflow.exe.config -ErrorAction SilentlyContinue
    Add-Content .\packages\SpecFlow.1.9.0\tools\specflow.exe.config "<?xml version=""1.0"" encoding=""utf-8"" ?>"
    Add-Content .\packages\SpecFlow.1.9.0\tools\specflow.exe.config "<configuration>"
    Add-Content .\packages\SpecFlow.1.9.0\tools\specflow.exe.config "<startup>"
    Add-Content .\packages\SpecFlow.1.9.0\tools\specflow.exe.config "<supportedRuntime version=""v4.0.30319"" />"
    Add-Content .\packages\SpecFlow.1.9.0\tools\specflow.exe.config "</startup>"
    Add-Content .\packages\SpecFlow.1.9.0\tools\specflow.exe.config "</configuration>"

    Exec { & .\packages\SpecFlow.1.9.0\tools\specflow.exe generateall .\tests\OpenMagic.Specifications\OpenMagic.Specifications.csproj /force }

    Write-Host
    Write-Host "Successfully generated SpecFlow tests."
}

Task Compile -depends Clean, Restore-NuGet-Packages, Generate-SpecFlow-Tests {

    Compile-Solution $sln $configuration
}

Task End-to-End-Tests -depends Compile {

    Run-xUnit-Tests -xunit .\packages\xunit.runners\tools\xunit.console.clr4.exe -testsFolder .\tests -configuration $configuration
}

FormatTaskName {
   param($taskName)

   Write-Host $taskName -ForegroundColor Yellow
   Write-Host "----------------------------------------------------------------------" -ForegroundColor Yellow
}