@echo off
dotnet restore --no-cache
IF ERRORLEVEL 1 GOTO error
dotnet build --no-restore
IF ERRORLEVEL 1 GOTO error
dotnet test --logger "console;verbosity=detailed"
IF ERRORLEVEL 1 GOTO error
GOTO end
:error
pause
:end
