# Einführung in ECMAScript 6 

## Unterlagen
- [Understanding ECMAScript 6 - R-5](http://www.r-5.org/files/books/computers/languages/escss/fp/Nicholas_C_Zakas-Understanding_ECMAScript_6-EN.pdf)

## Ausführen der Codebeispiele mit Node.js
Zu Beginn betrachten wir reinen Javascript Code. Dieser kann entweder online auf [jsfiddle](https://jsfiddle.net/)
oder mit Node.js ausgeführt werden. Um die Beispiele mit Node.js erstellen zu können, sind folgende 
Schritte notwendig:
1. Installation von Node.js von https://nodejs.org/en/. Es kann die aktuellste Version verwendet werden.
   Ändere den Pfad auf *C:\node*, denn kurze Pfadangaben machen das Leben leichter.
1. Nachdem Node.js installiert wurde, kannst du die Installation gleich prüfen. Öffne die Konsole und
   gib den Befehl `npm install -g jshint` ein. Es wird nun jshint installiert, sodass wir es in Visual
   Studio Code verwenden können.
1. Öffne Visual Studio Code und installiere die Extension *jshint*. 

Wenn nun in Visual Studio Code eine js Datei erstellt wird, kann im Menü mit *View* - *Output* das 
Ergebnis des Linters angezeigt werden. Mit *ALT* + *SHIFT* + *F* kann der Code automatisch formatiert
werden.

jshint muss für ECMAScript 6 noch konfiguriert werden. Dafür wird in jeder js Datei in der ersten Zeile
ein Kommentar erstellt. Dadurch kann jshint gesteuert werden:
```c#
/* jshint esversion: 6, strict:global */
/* globals console */
"use strict";   

let x = 1;
let y = a + 1;
```

Mit *F5* kann nun das Script im Debugmodus ausgeführt werden. Vorher können Haltepunkte links von den
Zeilennummern gesetzt werden.