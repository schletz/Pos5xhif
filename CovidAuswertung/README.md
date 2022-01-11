# Auswertung der COVID Fallzahlen

Auf https://www.data.gv.at/covid-19/ steht die Datei
*COVID-19: Zeitverlauf der gemeldeteten COVID-19 Zahlen der Bundesländer (Morgenmeldung)* zur
Verfügung (direkte URL: https://info.gesundheitsministerium.gv.at/data/timeline-faelle-bundeslaender.csv)

Dieses Jupyter Notebook visualisiert die Fallzahlen in der Datei.

## Installation

- Installiere die neueste Python Version von https://www.python.org/downloads/
- Aktualisiere mit `python -m pip install --upgrade pip` den Package Manager PIP
- Installiere danach die folgenden Pakete:

```text
pip install ipykernel
pip install requests
pip install numpy
pip install pandas
pip install matplotlib
pip install statsmodels
```

Danach setze in Visual Studio Code den Python interpreter (siehe https://code.visualstudio.com/docs/datascience/jupyter-notebooks#_setting-up-your-environment)

## Start

Öffne die Datei [covid_auswertung.ipynb](covid_auswertung.ipynb) in VS Code.

## Rohdaten

Die Datei *timeline-faelle-bundeslaender.csv* hat folgenden Aufbau
(Delimiter: ; Encoding: UTF8 mit BOM, Zeilenende ist CR+LF)

| Datum                     | BundeslandID | Name             | BestaetigteFaelleBundeslaender | Todesfaelle | Genesen | Hospitalisierung | Intensivstation | Testungen | TestungenPCR | TestungenAntigen |
|---------------------------|--------------|------------------|--------------------------------|-------------|---------|------------------|-----------------|-----------|--------------|------------------|
| 2021-03-01T09:30:00+01:00 | 1            | Burgenland       | 12657                          | 239         | 11701   | 43               | 10              | 638575    | 155435       | 483140           |
| 2021-03-01T09:30:00+01:00 | 2            | Kärnten          | 29225                          | 683         | 27316   | 96               | 15              | 675557    | 217933       | 457624           |
| 2021-03-01T09:30:00+01:00 | 3            | Niederösterreich | 74538                          | 1350        | 67900   | 349              | 82              | 3400756   | 1141984      | 2258772          |
| 2021-03-01T09:30:00+01:00 | 4            | Oberösterreich   | 86409                          | 1515        | 82567   | 120              | 20              | 2162517   | 546777       | 1615740          |
| 2021-03-01T09:30:00+01:00 | 5            | Salzburg         | 37327                          | 493         | 35318   | 71               | 15              | 823353    | 274598       | 548755           |
| 2021-03-01T09:30:00+01:00 | 6            | Steiermark       | 55956                          | 1747        | 51265   | 200              | 35              | 1758065   | 503274       | 1254791          |
| 2021-03-01T09:30:00+01:00 | 7            | Tirol            | 48060                          | 579         | 46304   | 81               | 21              | 1521757   | 579890       | 941867           |
| 2021-03-01T09:30:00+01:00 | 8            | Vorarlberg       | 23859                          | 274         | 23199   | 30               | 7               | 825612    | 298572       | 527040           |
| 2021-03-01T09:30:00+01:00 | 9            | Wien             | 92818                          | 1694        | 86446   | 363              | 85              | 3197153   | 1677203      | 1519950          |
| 2021-03-01T09:30:00+01:00 | 10           | Österreich       | 460849                         | 8574        | 432016  | 1353             | 290             | 15003345  | 5395666      | 9607679          |
| 2021-03-02T09:30:00+01:00 | 1            | Burgenland       | 12742                          | 240         | 11748   | 46               | 10              | 645941    | 155933       | 490008           |
| 2021-03-02T09:30:00+01:00 | 2            | Kärnten          | 29410                          | 686         | 27446   | 91               | 16              | 704089    | 219190       | 484899           |
| 2021-03-02T09:30:00+01:00 | 3            | Niederösterreich | 74893                          | 1357        | 68352   | 381              | 78              | 3471229   | 1147138      | 2324091          |
| 2021-03-02T09:30:00+01:00 | 4            | Oberösterreich   | 86664                          | 1518        | 82820   | 130              | 16              | 2241062   | 548394       | 1692668          |
| 2021-03-02T09:30:00+01:00 | 5            | Salzburg         | 37564                          | 496         | 35417   | 73               | 15              | 847370    | 275923       | 571447           |

## Übung

Visualisiere die Daten zur Impfung auf https://www.data.gv.at/katalog/dataset/276ffd1e-efdd-42e2-b6c9-04fb5fa2b7ea
(direktlink zum CSV auf https://info.gesundheitsministerium.gv.at/data/COVID19_vaccination_doses_timeline.csv)

Zeichne die Linien für die erste, zweite und dritte Dosis pro Bundesland. Hinweis: Mit *groupBy*
können die Daten pro Bundesland aufsummiert werden.
