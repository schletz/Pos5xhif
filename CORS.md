# REST mit Node.JS und ASP.NET Core

## Konfiguration des Servers

Zuerst muss bei den Services das Cors Service konfiguriert werden. Wir werden nur im Development
Mode Anfragen von allen URLs erlauben:

**Program.cs**
```c#
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
    });
}
```

Danach muss die Middleware aktiviert werden, sonst wird der CORS Header nicht gesendet. Dies
kann gleich als erste Middleware gesetzt werden:

**Program.cs**
```c#
app.UseCors();
```

## Konfiguration des Node Clients

### Erstellen einer Datei config.js

Damit Konfugirationseinstellungen wie die URL der API zentral verwaltet werden können, legen wir
im *src* Ordner eine Datei *config.js* an. Im Development Mode (mit *yarn serve* oder einem anderen
DEV Server) ist die API ein anderer Server, daher wird die URL hier konfiguriert. In Production
wird die Clientapplikation von ASP.NET Core Webserver ausgeliefert, daher reichen hier relative
URLs.

**config.js**
```javascript
const dev = {
    apiUrl: "https://localhost:7120"
};

const production = {
    apiUrl: ""
};

export default process.env.NODE_ENV == 'production' ? production : dev;
```

### Ein REST Service als Singleton

Nun wird im Ordner *src* ein neuer Ordner services angelegt. Danach wird die Datei *restService.js*
mit folgendem Inhalt erstellt:

```javascript
import conf from "../config.js"

export default {
    getUrl(url) {
        return `${conf.apiUrl}${url.startsWith("/") ? "" : "/"}${url}`;
    },
    getHeader() {
        const header = {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }
        if (this.token !== undefined) {
            header['Authorization'] = `Bearer ${this.token}`;
        }
        return header;
    },
    login(username, password) {
        return new Promise((resolve, reject) => {
            this.postJson("api/user/login", { username: username, password: password })
                .then(data => {
                    this.username = data.username;
                    this.token = data.token;
                    this.role = data.role;
                    resolve(data);
                })
                .catch(err => reject(err));
        });
    },
    getJson(url) {
        return this.sendRequest(url, 'GET');
    },
    postJson(url, data) {
        return this.sendRequest(url, 'POST', data);
    },
    sendRequest(url, method, data) {
        return new Promise((resolve, reject) => {
            const fetchParams = {
                method: method,
                headers: this.getHeader()
            }
            if (method != 'GET' && data !== undefined) { fetchParams.body = JSON.stringify(data); }
            fetch(this.getUrl(url), fetchParams)
                .then(response => {
                    if (response.ok) {
                        response.json().then(data => resolve(data));
                        return;
                    }
                    if (response.status == 400) {
                        response.json().then(data => { reject({ status: response.status, data: data }); })
                        return;
                    }
                    reject({ status: response.status });
                })
                .catch(() => reject({ status: 0 }));             // Server nicht erreichbar
        });

    }
}
```

### Nutzen in einer React Komponente

Die Methode Login setzt den internen State des Rest Services. Der Username, die Rolle und der
Token werden gespeichert.

```javascript
import rest from '../services/restService.js';

// ...
rest.login('myUser', 'myPass')
    .then(() => console.log(rest.username, rest.token, rest.role))
    .catch((err) => console.log(err));
```

Jetzt kann in jeder beliebigen Komponente das Rest Service mit *import* geladen werden.

```javascript
import rest from '../services/restService.js';


rest.getJson('/api/stores')
    .then((data) => console.log(JSON.stringify(data)))
    .catch((err) => console.log(err));

rest.postJson('/api/stores', {name: "New Store"})
    .then((data) => console.log(JSON.stringify(data)))
    .catch((err) => console.log(err));    
```
