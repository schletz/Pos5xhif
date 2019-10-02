# Functions und der Prototype

## Funktionen als *first-class objects*
Wir betrachten eine Funktion, die wir in die Variable *Person* schreiben. Da in Javascript alles ein
Objekt ist, können wir mit *this* auf die aktuelle Instanz zugreifen und zusätzliche Properties anlegen.
Diese Properties können wiederum Funktionen wie *getName1()* beinhalten: 
```js
const Person = function (zuname, vorname) {
    this.zuname = zuname;
    this.vorname = vorname;
    this._geburtstag = new Date();
    this.getName1 = function () {
        // Template Literals
        return `1: ${this.zuname} ${this.vorname}`;
    };
};

const p1 = new Person("ZN1", "VN1");
const p2 = new Person("ZN2", "VN2");

// Person {zuname: "ZN1", vorname: "VN1", getName1: }
// Person {zuname: "ZN2", vorname: "VN2", getName1: }
console.log(p1, p2);   
```

Bei der Ausgabe der beiden Instanzen wird *zuname*, *vorname* und *getName1()* angezeigt. Die Funktion wird
also 2x gespeichert. Um das zu verhindern, brauchen wir den sogenannten *Prototype*. Bei der Instanzierung
eines Objektes wird auf den Prototype verwiesen:

![](https://www.oreilly.com/library/view/learning-javascript-design/9781449334840/httpatomoreillycomsourceoreillyimages1547807.png)

<sup>Quelle: https://www.oreilly.com/library/view/learning-javascript-design/9781449334840/ch09s07.html</sup>

Um eine Methode zum Prototype einer Funktion hinzuzufügen, schreiben wir einfach eine Zuweisung:

```js
Person.prototype.getName2 = function() {
    return `2: ${this.zuname} ${this.vorname}`;
};

console.log(p1.getName1());   // 1: ZN1 VN1
console.log(p2.getName2());   // 2: ZN2 VN2
```

Interessant ist folgende Zuweisung. Hier wird quasi eine statische Methode definiert. Dadurch ist das 
Verwenden von *this* für den Zugriff auf Properties der Instanz natürlich nicht möglich:
```js
Person.getName3 = function() {
    return `3: ${this.zuname} ${this.vorname}`;
};

console.log(Person.getName3());   // 3: undefined undefined
```

## get und set
Wir können seit ECMAScript 5.1 auch Methoden als setter und getter verwenden, ähnlich den Properties
in C#. Beachte, dass in diesem Beispiel der Prototype der Funktion ersetzt wird, das bedeutet dass die
vorige Funktion *getName2()* ebenfalls im JSON Objekt enthalten sein muss. Die Instanzierung der Personenobjekte
darf erst nach diesen Anweisungen erfolgen:
```js
Person.prototype = {
    getName2: function () {
        return `2: ${this.zuname} ${this.vorname}`;
    },
    get geburtstag() {
        return this._geburtstag;
    },
    set geburtstag(value) {
        const min_date = new Date(1900,0,1).getTime();
        if (value.getTime() > min_date) {
            this._geburtstag = value;
        }
    },
    get alter() {
        return (Date.now() - this._geburtstag.getTime()) / (365.25 * 86400 * 1000);
    }
};

// Muss nachher sein!
const p1 = new Person("ZN1", "VN1");
const p2 = new Person("ZN2", "VN2");

p1.geburtstag = new Date(2000,0,1);
console.log(p1.geburtstag, p1.alter);   // Sat Jan 01 2000 00:00:00   19.75
```

Weitere Informationen zu get und set Methoden sind auf der [MDN](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/get).

## Praktisches Beispiel: Erweitern des Date Objektes
Folgendes Beispiel erweitert das integrierte *Date* Objekt um die statische Methode *fromToday()* und
die Methode *getDaystring()*.

```js
Date.weekdays = ["So", "Mo", "Di", "Mi", "Do", "Fr", "Sa"];
Date.fromToday = function () {
    return new Date(
        Math.trunc(Date.now() / 3600000) * 3600000
    );
};

Date.prototype.getDaystring = function () {
    const year = this.getUTCFullYear();
    const month = this.getUTCMonth() + 1;
    const day = this.getUTCDate();
    const weekday = this.getDay();
    return `${Date.weekdays[weekday]}, ${day}.${("0" + month).slice(-2)}.${year}`;
};

console.log(Date.fromToday().getDaystring());   // Mi, 2.10.2019
```

## Klassen in ECMAScript 6
Mit ECMAScript 6 werden diese Ideen syntaktisch mit Hilfe von *Klassen* umgesetzt. Es ist jedoch zu
beachten, dass das darunterliegende Konzept das der Function Objects ist und keine Neuimplementierung
darstellt.

Das folgende Beispiel zeigt die Umsetzung der Personenklasse in ECMAScript 6.

```js
/* jshint esversion: 6, strict:global */
/* globals console */

// Function und Prototype
"use strict";

class Person {
    constructor(zuname, vorname) {
        this.zuname = zuname;
        this.vorname = vorname;
        this._geburtstag = new Date();

        // Unsinnig, aber ist die Entsprechung der Methode aus dem vorigen Beispiel.
        this.getName1 = function () {
            // Template Literals
            return `1: ${this.zuname} ${this.vorname}`;
        };

    }
    // Wird im Prototype angelegt!
    getName2() {
        return `2: ${this.zuname} ${this.vorname}`;
    }
    get geburtstag() {
        return this._geburtstag;
    }
    set geburtstag(value) {
        const min_date = new Date(1900, 0, 1).getTime();
        if (value.getTime() > min_date) {
            this._geburtstag = value;
        }
    }
    get alter() {
        return (Date.now() - this._geburtstag.getTime()) / (365.25 * 86400 * 1000);
    }
}

// Auch nicht sehr sinnvoll, ist aber möglich und die Entsprechung von getName3 aus dem vorigen Beispiel.
Person.getName3 = function () {
    return `3: ${this.zuname} ${this.vorname}`;
};

const p1 = new Person("ZN1", "VN1");
const p2 = new Person("ZN2", "VN2");

// getName1 ist 2x angelegt.
console.log(p1, p2);
console.log(p1.getName1());
p1.geburtstag = new Date(2000, 0, 1);
console.log(p1.geburtstag, p1.alter);   // Sat Jan 01 2000 00:00:00   19.75
console.log(Person.getName3());
```

