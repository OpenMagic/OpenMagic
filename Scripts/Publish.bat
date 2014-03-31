@echo off

echo.
echo Checking out develop...
echo.
git checkout develop
if not "%errorlevel%" == "0" exit %errorlevel%

echo.
echo Pushing develop...
echo.
git push
if not "%errorlevel%" == "0" exit %errorlevel%

echo.
echo Checking out master...
echo.
git checkout master
if not "%errorlevel%" == "0" exit %errorlevel%

echo.
echo Merging develop into master...
echo.
git merge develop
if not "%errorlevel%" == "0" exit %errorlevel%

echo.
echo Pushing master...
echo.
git push
if not "%errorlevel%" == "0" exit %errorlevel%

echo.
echo Switching back to develop...
echo.
git checkout develop
if not "%errorlevel%" == "0" exit %errorlevel%

exit 0