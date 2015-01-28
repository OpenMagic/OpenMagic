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

Task Compile -depends Clean, Restore-NuGet-Packages {

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