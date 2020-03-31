using ContractLibrary;           //  Add Reference auf ContractLibrary!
using SoapServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoapServer.Services
{
	/// <summary>
	/// Implementiert die serverseitige Logik des Services. Es ist die einzige Klasse, die das
	/// Interface implementiert. Am Client wird nur das Interface genutzt.
	/// </summary>
	public class CalcService : ICalcService
	{
		/// <summary>
		/// Addiert 2 Zahlen und trägt das Ergebnis der Berechnung in der Datenbank ein.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Add(int x, int y)
		{
			var newCalc = new Calculation
			{
				A = x,
				B = y,
				C = 0,
				Result = x + y,
				Operation = "+"
			};

			using (SoapDatabase db = new SoapDatabase())
			{

				db.Calculations.Add(newCalc);
				db.SaveChanges();
			}
			return newCalc.Result;
		}

		/// <summary>
		/// Liefert eine Statistik, wie viele Berechnungen pro Operation durchgeführt wurden.
		/// </summary>
		/// <returns></returns>
		public List<CalcStats> GetCalculationCount()
		{
			using (SoapDatabase db = new SoapDatabase())
			{
				return (from c in db.Calculations
						group c by c.Operation into g
						select new CalcStats
						{
							Operation = g.Key,
							Count = g.Count()
						}).ToList();
			}
		}
	}
}
