using System;
using System.Collections.Generic;
using System.ServiceModel;  // Install-Package System.ServiceModel.Http und
                            // Install-Package System.ServiceModel.Primitives
using ContractLibrary;//  Add Reference auf ContractLibrary!

namespace SoapClient
{
    /// <summary>
    /// Sendet einen Request an den Server, indem es eine Methode des angebotenen Services aufruft.
    /// STARTEN DES PROGRAMMES:
    /// Im Solution Explorer im Kontextmenü der Solution den Punkt Properties wählen.
    /// Dann Multiple Startup Projects festlegen, um SoapClient und SoapServer gleichzeitig zu starten.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Die Adresse, unter der der Server erreichbar ist. Konfigurierbat in Program.cs des
        /// SoapServer (in CreateHostBuilder)
        /// </summary>
        static string BaseUrl => "http://localhost:8080";

        static void Main(string[] args)
        {
            try
            {
                // Öffnet die Http Verbindung und ruft CalcService.svc auf. Diese Route muss in Configure
                // der Datei Startup.cs registiert werden.
                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress endpoint = new EndpointAddress($"{BaseUrl}/CalcService.svc");
                // Jetzt kommt das Interface ins Spiel. Der Request ruft Methoden von ICalcService
                // auf, deswegen verwenden wir hier das Interface.
                ChannelFactory<ICalcService> channelFactory = new ChannelFactory<ICalcService>(binding, endpoint);
                ICalcService serviceClient = channelFactory.CreateChannel();

                // Nun können wir ganz normal Methoden aufrufen, wie wenn wir eine lokale
                // Klasse hätten, die das Interface implementiert.
                int result = serviceClient.Add(1, 3);
                List<CalcStats> calcCount = serviceClient.GetCalculationCount();
                channelFactory.Close();

                Console.WriteLine($"=========================");
                Console.WriteLine($"Das Ergebnis ist {result}");
                foreach (var c in calcCount)
                {
                    Console.WriteLine($"Operator {c.Operation}: {c.Count} Berechnungen durchgeführt.");
                }
            }
            catch (CommunicationException e)
            {
                Console.Error.WriteLine($"Keine Verbindung zum Server. Läuft der Server auch auf {BaseUrl}?");
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.InnerException?.Message);
                Console.Error.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.InnerException?.Message);
                Console.Error.WriteLine(e.StackTrace);
            }
        }
    }
}
