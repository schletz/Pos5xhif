@echo off
SET ZIP="C:\Program Files\7-Zip\7z.exe"

del /F /Q ..\SPG_Fachtheorie.7z
%ZIP% a -mx=9 ..\SPG_Fachtheorie.7z ..\SPG_Fachtheorie  -xr!.vs 
pause
