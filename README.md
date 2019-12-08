# POS Unterricht in den 5. HIF Klassen

Lehrinhalte gem. Lehrplan BGBl. II Nr. 262/2015 für den 5. Jahrgang. Die kursiv gedruckten Teile in der 
Spalte Lehrplaninhalt kennzeichnen die wesentlichen Punkte im Sinne der LBVO.

Die Punkte unter Umsetzung betreffen einen zweistündigen Teil des POS Unterrichtes. Der Rest bzw. Ergänzungen 
werden im anderen zweistündigen Teil unterrichtet.

| Lehrplaninhalt                                                                                                                                                                                	| Umsetzung                                                                                                                                                                     	| 
| ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	| 
| Komplexe, plattformübergreifende Softwaresysteme für den Produktivbetrieb erstellen: *Anwendung aktueller Softwaretechnologien*, Softwarequalität, Performance-Tests.                           	| Funktionale Programmierung in ECMAScript 6: Collections funktional bearbeiten (filter, map, reduce, ...), Funktionale Programmierung (Closures, Currying), Klassen, Funktionen	| 
| Plattformübergreifende Softwaresysteme erstellen: *Einsatz aktueller Programmiertechniken*, Internationalisierung, Optimierung, Systemtests, Deployment.                                        	| Views erstellen mit Vue.js                                                                                                                         	| 
| Komplexe Benutzerschnittstellen unter dem Aspekt der Usability entwerfen und implementieren: Visuelle Gestaltung und Dialoggestaltung, Softwareergonomie.                                     	| Visualisierungen: Darstellen von JSON Daten in Highcharts, Darstellen von JSON Daten in Leaflet.<br>React: Komponentenbasierende Entwicklung, isomorphe Webapplikationen (serverseitiges JavaScript)       	| 
| Verschiedene Softwarearchitekturen beschreiben und für konkrete, in der Praxis auftretende, Problemstellungen entsprechende Architekturen erstellen: *Softwarearchitekturen*, Architekturmuster.	|                                                                                                                                                                               	| 

## Beurteilungskriterien
Leistungsbeurteilung für das Wintersemester
- PLF am 8. Jänner 2020, alternativ:
- Einer oder eine Gruppe stellt eine Technologie vor,
  die er oder das Team verwendet hat.
  - Kein Powerpoint, live Coding bzw. Snippets.
  - Unterlagen für die Klasse in Form von Code, ...
  - Dauer: mind. 20 min pro Person.
  - Termine: bis spätestens 22. Jänner. Jeder kann sich
    kurzfristig zu Beginn der Stunde anmelden.
  
  

## Wichtiges zum Start
1. [Javascript mit Node.js und VS Code](01_ECMAScript6/README.md)
1. [Markdown Editing mit VS Code](https://github.com/schletz/Pos3xhif/blob/master/markdown.md)

### Synchronisieren des Repositories in einen Ordner
1. Lade von https://git-scm.com/downloads die Git Tools (Button *Download 2.xx for Windows*)
    herunter. Es können alle Standardeinstellungen belassen werden, bei *Adjusting your PATH environment*
    muss aber der mittlere Punkt (*Git from the command line [...]*) ausgewählt sein.
2. Lege einen Ordner auf der Festplatte an, wo du die Daten speichern möchtest 
    (z. B. *C:\Schule\POS\Examples*). Das
    Repository ist nur die lokale Version des Repositories auf https://github.com/schletz/Pos5xhif.git.
    Hier werden keine Commits gemacht und alle lokalen Änderungen dort werden bei der 
    nächsten Synchronisation überschrieben.
3. Initialisiere den Ordner mit folgenden Befehlen, die du in der Konsole in diesem Verzeichnis
    (z. B. *C:\Schule\POS\Examples*) ausführst:
```bash {.line-numbers}
git init
git remote add origin https://github.com/schletz/Pos5xhif.git
```

4. Lege dir in diesem Ordner eine Datei *syncGit.cmd* mit folgenden Befehlen an. 
    Durch Doppelklick auf diese Datei wird immer der neueste Stand geladen. Neu erstellte Dateien
    in diesem Ordner bleiben auf der Festplatte, geänderte Dateien werden allerdings durch 
    *git reset* auf den Originalstand zurückgesetzt.
```bash {.line-numbers}
git fetch --all
git reset --hard origin/master
```



