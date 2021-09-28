# Erstellen des Projektes
Gehe in einen Ordner deiner Wahl und führe in der Konsole die folgenden Befehle aus:
```text
rd /S /Q ScsOnlineShop
md ScsOnlineShop
cd ScsOnlineShop
md ScsOnlineShop.Application
md ScsOnlineShop.Test
md ScsOnlineShop.Webapp
cd ScsOnlineShop.Application
dotnet new classlib
dotnet add package Microsoft.EntityFrameworkCore --version 5.0.10
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 5.0.10
cd ..\ScsOnlineShop.Test
dotnet new xunit
dotnet add reference ..\ScsOnlineShop.Application
cd ..
dotnet new sln
dotnet sln add ScsOnlineShop.Application
dotnet sln add ScsOnlineShop.Test
start ScsOnlineShop.sln
```