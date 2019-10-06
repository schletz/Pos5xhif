# 1. Übung: Templates und Binding mit Vue.js

Das Konzept des Bindings ist schon aus der 3. Klasse bekannt. In XAML wurde z. B. ein Textfeld dynamisch
befüllt:
```xml
<TextBlock Text = "{Binding Name}" />
```

Im dazugehörigen Viewmodel muss es ein entsprechendes Property *Name* geben, dessen Inhalt im Textblock
angezeigt wird:
```cs
public class MainViewModel 
{
    public string Name {get; set; } = "My Name";
}
```

Vue.js verfolgt diese Ideen der MVVM (Model - View - Viewmodel) Architektur und setzt diese in Javascript
um. Betrachten wir die Ausgabe der Überschrift in *index.html* im Ordner *wwwroot*:
```html
<div id="myFirstApp">
    <h1>{{ title }}</h1>
</div>
```

Statt eines statischen Inhaltes wird hier in 2 geschweiften Klammern ein Variablenname geschrieben. Damit
dieser an ein Viewmodel gebunden werden kann, wird am Ende des Dokumentes auf eine Javascript Datei verwiesen:
```html
<script src="index.vm.js"></script>
```

Ein minimales Viewmodel, welches einen Wert für *title* liefert, sieht in Vue.js so aus:
```js
const vm = new Vue({
  el: '#myFirstApp',
  data: {
    title: "Beispiel 1: Binding mit Vue.js"
  }
});
```
Syntaktisch wird mit *new* eine Instanz von Vue erstellt. Sie ist in der Datei *vue.js* definiert, die
in *index.html* eingebunden wird:
```html
<script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
```

Wir erkennen 2 Properties, die beim Erstellen der Instanz übergeben werden: *el* bezieht sich auf den
Container, für den das Viewmodel gelten soll. Da das *&lt;div&gt;* Element mit einer id benannt wurde, ist die
Raute - wie in jQuery - voranzustellen. Das Objekt *data* ist ein JSON Objekt mit Properties, auf die
in HTML verwiesen werden kann.

## Templatesyntax
###  Variablen für input Felder
Unser Eingabefeld hat ein besonderes Attribut in der Deklaration. Mit *v-model* geben wir das Property
innerhalb des *data* Objektes des Viewmodels an, dessen Inhalt ausgelesen und beschrieben wird:
```html
<input v-model="newName">
```

### Ein- und Ausblenden mit v-if, Computed Properties
Soll ein Feld bedingt ein- und ausgeblendet werden, so kann mit *v-if* auf eine Funktion im Viewmodel verwiesen
werden. Diese Funktion gibt true bzw. false zurück, je nach dem, ob das Element ein- oder ausgeblendet
werden soll.
```html
<p v-if="hasName">Hello {{ newName }}</p>
```

Im Viewmodel sind dies sogenannte *Computed Properties*. Diese Properties sind Funktionen, die beliebigen
Code beinhalten können. Der Zugriff auf die Properties des Viewmodels ist einfach, beim Aufruf wird *this*
auf das *data* Objekt des Viewmodels gesetzt:
```js
computed: {
    hasName: function () {
        return this.newName != "";
    }
}
```

Bei einfachen Logiken kann ein Javascript Ausdruck auch gleich als Wert in HTML geschrieben werden:
```html
<div v-if="names.length > 0"></div>
```

### Eventhandling mit *v-on*
Unser Button muss natürlich auf Klickevents reagieren. Hier bietet Vue.js mit dem Attribut *v-on* eine
einfache Möglichkeit, auf Events (hier *click*) zu reagieren. Der Wert *addName* zeigt auf eine Methode im
Viewmodel:
```html
<button type="submit" v-on:click="addName">Add</button>
```

Diese Methode wird im Viewmodel unter *methods* deklariert. Damit das Eingabefeld nach dem Speichern
in der Liste wirder geleert wird, wird einfach das Property *newName*, auf das sich das Eingabefeld bindet,
auf einen Leerstring gesetzt.
```js
methods: {
  addName: function (event) {
    if (this.newName != "") { this.names.push(this.newName); }
    this.newName = "";
  }
}
```

### Iterationen mit *v-for*
Oft soll durch ein Array iteriert werden, um eine Liste oder Tabelle aufzubauen. Mit v-for kann ein Ausdruck
ähnlich der *foreach* Schleife in C# definiert werden. *names* ist hierbei das Property im Viewmodel, *name*
die Veriable, die für jeden Schleifendurchlauf neu gesetzt wird.
```html
<li v-for="name in names">
    {{ name }}
</li>
```

## Übung
Erweitere das Beispiel um ein 2. Eingabefeld, sodass Vor- und Zuname eingegeben werden kann. Das Layout
ist frei gestaltbar. Die Daten sollen im Viewmodel unter *names* als JSON Array in der Form
```js
{nr: 1, firstname: "Max", lastname: "Mustermann"}
```

anstatt eines einfaches Stringarrays gespeichert werden. Mit *{{ myName.firstname }}* kann in HTML auf ein
Property verwiesen werden.

Die Ausgabe der gespeicherten Namen soll in Form der folgenden Tabelle erscheinen. Ist kein Name gepsichert,
so soll die Tabelle ausgeblendet werden.
```html
<table>
    <tr><th>Nr</th><th>Vorname</th><th>Zuname</th></tr>
    <tr><td>1</td><td>(Vorname1)</td><td>(Zuname1)</td></tr>
    <tr><td>2</td><td>(Vorname2)</td><td>(Zuname2)</td></tr>
</table>
```

## Weitere Informationen
- [Vue.js Template Syntax](https://vuejs.org/v2/guide/syntax.html)
- [The Vue Instance](https://vuejs.org/v2/guide/instance.html)
- [Computed Properties and Watchers](https://vuejs.org/v2/guide/computed.html)
- [List Rendering](https://vuejs.org/v2/guide/list.html)