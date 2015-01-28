@echo off

pushd %~dp0

rem The following git clean will have not real effect on MyGet server.
rem What is will do is make developer's directory structure same as on
rem MyGet server.
git clean -d -X --force ..\

call powershell .\build.ps1

echo.
echo.
echo ERRORLEVEL: %errorlevel%
echo.
echo.

popd
