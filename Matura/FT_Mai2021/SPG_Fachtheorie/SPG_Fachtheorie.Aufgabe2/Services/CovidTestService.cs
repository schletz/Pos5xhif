using SPG_Fachtheorie.Aufgabe1.Domain;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    /// <summary>
    /// Implementierung der Aufgabe 2.
    /// Die Datenbankimplementierung ist mit einer dll verknüpft und
    /// unabhängig von Ihrer Implementierung funktionstüchtig. Aufgabe1 verweist auf diese
    /// DLL, nicht auf Ihr Projekt.
    /// Verwenden Sie zum Testen die in CovidTestServiceTests vorgegebenen Testmethoden.
    /// </summary>
    public class CovidTestService
    {
        private readonly CovidTestContext _db;

        public CovidTestService(CovidTestContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Berechnet die Teststatistik lt. Aufgabe 2c. Tauschen Sie object durch einen geeigneten Rückgabetyp aus.
        /// Hinweise:
        ///    Das Alter können Sie zur Vereinfachung mit Stichtag 1.1.2021 berechnen (Alter = 2021 - Geburtsjahr)
        ///    Die Kategorien können numerisch ausgegeben werden:
        ///        0: <10, 1: <20, 2: <30, 3: <40, 4: <50, 5: <60, 6: >60
        ///    Die relative Anzahl ist als Prozentwert (0 - 100) relativ zu den gesamten Tests in der Testtabelle auszugeben.
        ///    Es ist keine Rundung vorzunehmen, der Prozentwert kann in voller Genauigkeit zurückgegeben werden.
        /// </summary>
        public List<object> GetTestStatistics()
        {
            throw new NotImplementedException();
        }
    }
}
