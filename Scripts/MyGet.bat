@echo off

pushd %~dp0\..\

echo Current directory: 
cd
echo.

echo Setting up build environment...
echo -------------------------------
echo.

set config=%1
if "%config%" == "" (
   set config=Release
)

if "%PackageVersion%" == "" (

  rem Simulate myget.org environment.
  set PackageVersion=999.99
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

echo Building solution...
echo -------------------------------
echo.

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild /p:Configuration=%config%
if not "%errorlevel%" == "0" goto Error
echo.
echo.

echo Running unit tests...
echo -------------------------------
echo.

if "%GallioEcho%" == "" (

  echo Running tests with mstest...
  "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\mstest.exe" /testcontainer:Projects\OpenMagic.Tests\bin\Release\OpenMagic.Tests.dll"
  
) else (

  echo Running tests with Gallio...
  "%GallioEcho%" Projects\OpenMagic.Tests\bin\Release\OpenMagic.Tests.dll
)

if not "%errorlevel%" == "0" goto Error
echo.
echo.

echo Building NuGet package...
echo -------------------------
echo.

if exist .\Build\nul (

  echo .\Build folder exists.
  
) else (

  echo Creating .\Build folder...
  md .\Build
)

echo.
echo Creating NuGet package...

.\.nuget\nuget pack .\.nuget\OpenMagic.nuspec -o .\Build %Version%

if not "%errorlevel%" == "0" goto Error
echo.
echo.

:Success
echo.
echo.
echo Build was successful.
echo =====================
echo.
echo.
popd
exit 0

:Error
echo.
echo.
echo **************************
echo *** An error occurred. ***
echo **************************
echo.
echo.
popd
exit -1