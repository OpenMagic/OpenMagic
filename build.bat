@echo off

:start
call powershell .\build.ps1

echo.
echo.
choice /m "Do you want to re-run the build?"
if errorlevel 2 goto:eof
if errorlevel 1 goto:start
