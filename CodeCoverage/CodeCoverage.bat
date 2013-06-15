@echo off
rem Creates code coverage reports for %1

if "%1"=="" goto Usage

cd ..\

path=%path%;.\Packages\OpenCover.4.5.1604;.\Packages\ReportGenerator.1.8.1.0;

call "C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x86
if errorlevel 1 goto Error

if not exist .\CodeCoverage\%1\nul md .\CodeCoverage\%1
if errorlevel 1 goto Error

if exist .\CodeCoverage\%1\nul del /s /q .\CodeCoverage\%1
if errorlevel 1 goto Error

msbuild OpenMagic.sln /property:Configuration=Release /verbosity:minimal /nologo
if errorlevel 1 goto Error

OpenCover.Console -register:user -mergebyhash -target:"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\mstest.exe" -targetargs:"/TestContainer:.\Projects\%1.Tests\bin\Release\%1.Tests.dll /TestSettings:.\OpenMagic.testsettings" -output:.\CodeCoverage\%1\%1.xml -filter:"+[%1*]* -[%1.Tests*]* -[%1*]*.My.*"

if errorlevel 1 goto Error

ReportGenerator.exe .\CodeCoverage\%1\%1.xml .\CodeCoverage\%1
if errorlevel 1 goto Error

goto End

:Usage
echo.
echo.
echo Usage: CodeCoverage ProjectName
goto End

:Error
echo.
echo.
echo **************************************************************
echo **************************************************************
echo **************************************************************
echo ************** AN ERROR HAS OCCURRED. FIX IT!!!! *************
echo **************************************************************
echo **************************************************************
echo **************************************************************

:End
echo.
echo.
pause
