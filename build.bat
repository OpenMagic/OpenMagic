@echo off
pushd %~dp0

:start
cls
call powershell .\build\build.ps1

echo.
echo.
choice /m "Do you want to re-run the build?"
if errorlevel 2 goto finish
if errorlevel 1 goto start

:finish
popd
