Framework "4.0"

properties {
    $sln = "OpenMagic.sln"
    $initialPath = $env:Path
}

task default -depends RunAllTests

tasksetup {

    Push-Location ..\

    if (Test-Path $sln)
    {
        Write-Host "Running build tasks from $(Get-Location) folder."
        Write-Host
    }
    else
    {
        throw "Cannot find $sln in $(Get-Location)."
    }

	$ide = ""
    $ides = @("C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE", "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE", "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE")

	foreach ($ide in $ides)
	{
		if (Test-Path "$ide\mstest.exe")
		{
			break
		}
		else
		{
			$ide = ""
		}
	}
	
	if ($ide -eq "")
    {
        throw "Cannot find mstest.exe in $ides."
    }

    $env:Path = $env:Path + ";" + $ide + ";"
}

taskteardown {

   Pop-Location
   $env:path = $initialPath
}

task Clean {

    Get-ChildItem -include bin,obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }
}

task Compile -depends Clean {

    Exec { msbuild $sln /verbosity:normal /property:Configuration=Release }
}

task Pull { 

  Exec { git pull }
}

task Push { 

  Exec { git push }
}

task RunAllTests -depends RunUnitTests, RunIntegrationTests {
}

task RunIntegrationTests -depends Compile {
}

task RunUnitTests -depends Compile {

    # todo: Generic method to find all test projects.
    Exec { mstest /testcontainer:Projects\OpenMagic.Tests\bin\Release\OpenMagic.Tests.dll }
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}
