@echo off
choice /m "Are you sure you want to delete all files & directories not tracked by git?"
if errorlevel 2 goto:eof
git clean -d --force -X ..\
pause
