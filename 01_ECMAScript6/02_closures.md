# Closures und Currying
Für unsere Beispiele verwenden wir 2 JSON Arrays, die Klassen und ihren Stundenplan speichern:
```javascript
/* jshint esversion: 6, strict:global */
/* globals console */
"use strict";

const klassen_data = [{ name: "5AHIF" }, { name: "5BHIF" }];

const stdplan_data = [
    { klasse: "5AHIF", tag: 1, stunde: 1, fach: "NVS" },
    { klasse: "5AHIF", tag: 1, stunde: 2, fach: "POS" },
    { klasse: "5AHIF", tag: 1, stunde: 3, fach: "BWM" },
    { klasse: "5AHIF", tag: 2, stunde: 4, fach: "DBI" },
    { klasse: "5AHIF", tag: 2, stunde: 6, fach: "NVS" },
    { klasse: "5BHIF", tag: 3, stunde: 7, fach: "E" },
    { klasse: "5BHIF", tag: 3, stunde: 9, fach: "D" },
    { klasse: "5BHIF", tag: 4, stunde: 1, fach: "AM" },
    { klasse: "5BHIF", tag: 4, stunde: 2, fach: "NVS" }
];
```

## Closures
Wir möchten nun aus dem Stundenplan für die Anzeige einzelne Klassen filtern. Der "klassische" Ansatz
wäre eine Methode, die aus dem Array die Klassen filtert:
```javascript
function getLessons(klasse) {
    return stdplan_data.filter(s => s.klasse == klasse);
}
```

Wollen wir die Stunden der Klasse an einem bestimmten Tag herausfinden, können wir nachträglich nach
dem entsprechenden Tag filtern. Alternativ können wir auch die Methode *getLessons()* mit einem 2.
Parameter versehen.

Der funktionale Ansatz erlaubt uns flexibler zu agieren. Folgendes Beispiel gibt nicht nur die Daten
zurück, sondern ein JSON Objekt mit Methoden, die basierend auf diesen gefilterten Daten agieren:
```javascript
function getLessons(klasse) {
    console.log("Methode getLessons");

    let lessons = stdplan_data
        .filter(l => l.klasse == klasse);

    return {
        getAll() {
            console.log("Methode getAll");
            return lessons;
        },

        getDay(day) {
            console.log("Methode getDay");
            return lessons.filter(l => l.tag == day);
        }
    };
}
```

Wollen wir nun die Stunden der 5AHIF herausfinden, so können wir diese durch den Aufruf von *getLessons()*
filtern:
```javascript
console.log("Aufruf von getLessons");
// Speichert das JSON Objekt für die 5AHIF.
let lessons_5ahif = getLessons("5AHIF");
```

Beachte die Ausgabe. In der Konsole wird folgendes geschrieben:
```
Aufruf von getLessons
Methode getLessons
```

Die Methoden *getAll()* bzw. *getDay()* werden *nicht* aufgerufen. Sie sind nur im zurückgegebenen
JSON Objekt definiert.

Möchten wir nun die Stunden der 5AHIF am Montag herausfinden, so können wir die angelegte Veriable
*lessons_5ahif* verwenden:
```javascript
console.log("Aufruf von getDay");
let monday_lessons = lessons_5ahif.getDay(1);
```

Die Konsolenausgabe zeigt 2 Zeilen:
```
Aufruf von getDay
Methode getDay
```

Die Methode *getLessons()* wird nicht mehr aufgerufen. 

Dieses Beispiel führt uns zur Definition von Closures:
> A closure is a function having access to the parent scope, even after the parent function has closed.
> (https://www.w3schools.com/js/js_function_closures.asp)

Warum ist so etwas wichtig? Die MDN gibt einen Hinweis über die Hauptverwendung dieser Eigenschaft:
> Closures are useful because they let you associate some data (the lexical environment) with a function 
> that operates on that data. This has obvious parallels to object-oriented programming, where objects 
> allow us to associate some data (the object's properties) with one or more methods.
> (https://developer.mozilla.org/en-US/docs/Web/JavaScript/Closures)


### Übung zu Closures
Definiere die aufgerufenen Funktionen so, dass die nachfolgenden Codezeilen ausgeführt werden können und
das korrekte Ergebnis liefern:
```javascript
getWeekday(1).getClasses()                  // Liefert alle Klassen als string Array, die am Montag Unterricht haben.
getWeekday().getClasses()                   // Liefert alle Klassen als string Array, die im Stundenplan vorkommen.
getWeekday(1).getClass("5AHIF").getCount()  // Liefert die Anzahl der Stunden der 5AHIF am Montag.
getWeekday(1).getClass("5BHIF").count;      // count ist ein normales Property, keine Funktion.
```

Hinweise: 
- Um ein Array mit eindeutigen Werten zu erstellen, kann der Umweg über das in ECMAScript 6
  enthaltene *Set* gemacht wenrden: `const unique = Array.from(new Set([1, 2, 3, 4, 5, 1]));`
- Defaultparameter könenn mit ECMAScript 6 so wie in C# definiert werden: `function getWeekday(day = 0)`
  schreibt den Wert 0 in *day*, wenn die Funktion ohne Parameter aufgerufen wurde.
