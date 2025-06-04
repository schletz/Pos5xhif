@echo off
chcp 65001 >nul
set PROJECTS=test\SPG_Fachtheorie.Aufgabe1.Test test\SPG_Fachtheorie.Aufgabe2.Test test\SPG_Fachtheorie.Aufgabe3.Test
for %%P in (%PROJECTS%) do (
    dotnet build %%P --no-restore > nul 2>&1
    if errorlevel 1 (
        echo ❌ Build von %%P kompiliert nicht. Es kann nicht gewertet werden!
    ) else (
        echo ✅ Build von %%P erfolgreich! Glückwunsch!
    )
    echo.
)

pause
