
# Fachtheorie POS1 Haupttermin 2020/2021


## Ihre Mission ;)

Implementieren Sie die Aufgabenstellung.

Das Ergebnis **muss** am `p:` (`\\enterprise\exams\home\<Raum>\ihr Arbeitsplatz<>`) 
Laufwerk liegen - ausschließlich Abgaben via `p:` können und werden berücksichtigt.

Die Projektvorlage - welche kopiert werden muss - finden Sie unter: `r:\exams\labore\FT_POS1_Java_2021`

Die Projektvorlage selbst ist das Verzeichnis: `2021-ft-pos1` (diese nach `p:` kopieren)
und das erforderliche Maven Repository: `repository` (muss nicht kopiert werden).


## Dokumentation

Sie finden einige reference manuals im folder `documentation`.


## Projekt Setup

* Java Version: 11
* Spring Boot Version: 2.4.6

Es wurde ein Hauptprojekt erstellt, welches dazu dient die drei
Sub Projekte zusammen zu fassen und einmalig die erforderlichen
Abhängigkeiten zu definieren.

Die eigentliche Implementierung soll aussschliesslich in den drei
Sub Projekten stattfinden.


## IntelliJ Settings

### Lombok Plugin installed

`Settings` -> `Plugins`

`Lombok` installed

### Enable annotation processing to use Lombok

`Settings` -> `Build, Execution, Deployment` -> `Compiler` -> `Annotation Processors`

`Enable Annotation Processing`

### Maven Local Repository

`Settings` -> `Build, Execution, Deployment` -> `Build Tools` -> `Maven`

`Local repository` with `R:\exams\labore\FT_POS1_Java_2021\repository`
