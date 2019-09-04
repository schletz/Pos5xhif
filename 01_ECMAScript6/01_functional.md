# Funktionales Programmieren in ECMAScript 6
Alle Beispiele basieren auf einem Array mit 4 Personen:
```javascript
const persons = [
    {name: "Name1", age: 15, sex: "f"},
    {name: "Name2", age: 17, sex: "m"},
    {name: "Name3", age: 19, sex: "f"},
    {name: "Name4", age: 21, sex: "m"}
];

```

## Filtern (Where in LINQ)
Finde alle weiblichen Personen heraus. Viele erledigen dies noch vollständig imperativ, also mittels
```javascript
for(let i = 0; i < persons.length; i++) {
	if (persons[i].sex == "f") {
  	// Do something
  }
}
```

Auf der [MDN Seite](https://developer.mozilla.org/de/docs/Web/JavaScript/Reference/Global_Objects/Array)
über Arrays finden sich Methoden, die uns einen funktionalen Zugang zu Daten ermöglichen: *filter*, *find*, *map*, *reduce*, *every* und *some*

Nun können wir eine Funktion *female_filter* definieren, die - so wie *Where* in LinQ - das Filterkriterium
beinhaltet.

```javascript
function female_filter(item) {
	return item.sex == "f";
}

console.log(persons.filter(female_filter));
```

Natürlich ist diese Methode recht mühsam, doch dafür gibt es in ECMAScript 6 die sogenannten 
[Arrow functions](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/Arrow_functions).
Damit lässt sich der Ausdruck drastisch verkürzen:

```javascript
console.log(persons.filter(item=>item.sex == "f"));
```

## Map (Select in LINQ)
*map* wird für jedes Element im Array aufgerufen. Es wird ein Array zurückgegeben, wo statt dem
Originalwert der jeweilige Rückgabewert steht:

```javascript
let ages = persons
  .map(item=>item.age);
  
console.log(ages); // Liefert  [15, 17, 19, 21]
```

Natürlich können wir auch zu den einzelnen Datensätzen etwas dazugeben, allerdings müssen wir bei
der Arrow Function das *return* explizit setzen:

```javascript
let ages = persons
  .map(item=> { item.is18 = item.age >= 18; return item; } );
  
console.log(ages);
```

## Akkumulieren mit *reduce*
Oft möchten wir ein Array auf einen einzelnen Wert abbilden (z. B. summieren, ...). Mittels *reduce*
wird der Funktion ein Akkumulator übergeben. Der neue Wert wird in der Callback Funktion von *reduce*
einfach gesetzt und zurück gegeben.

Nähere Infos sind auf der [MDN](https://developer.mozilla.org/de/docs/Web/JavaScript/Reference/Global_Objects/Array/Reduce)
zu finden.

Folgendes Beispiel berechnet die Summe des Alters aller Frauen.

```javascript
let ages = persons
  .filter(item => item.sex >= "f")
  .map(item => item.age)
  .reduce((accum, item) => accum + item, 0);

console.log(ages);  // 72
```

## Übung
Speichere statt dem Property *age* das Property *dateOfBirth*. Es soll vom Typ *Date* sein, in der
[MDN](https://developer.mozilla.org/de/docs/Web/JavaScript/Reference/Global_Objects/Date) gibt es
Informationen über das Anlegen von Datumsobjekten. Achte auf den Monatsindex (beginnend mit 0)!

Filtere nun aller Personen, die 18 Jahre als sind, aus. Deklariere dafür deine Filterfunktion explizit und
mit folgenden Überlegungen: Die Funktion soll *pure* sein, d. h. sie darf nicht den aktuellen Tag
innerhalb der Funktion ermitteln. Dieser muss übergeben werden. Folgendes Snippet zeigt eine Funktion,
die eine variable Altersfilterung für unser Personenobjekt ermöglicht:

```javascript
function age_filter(minAge) {
	return function(item) {
    	return item.age >= minAge;
  }
}

let over18 = persons
	.filter(age_filter(18))
  
console.log(over18);
```

Link: [Functional Programming in JavaScript](https://www.freecodecamp.org/news/functional-programming-principles-in-javascript-1b8fc6c3563f/)