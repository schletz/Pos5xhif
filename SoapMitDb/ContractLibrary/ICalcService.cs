using System;
using System.Collections.Generic;
using System.ServiceModel;  // Install-Package System.ServiceModel.Primitives
using System.Text;


namespace ContractLibrary
{
	/// <summary>
	/// Definiert ein Interface, der alle Methoden meines Services, welches ich über SOAP erreichen
	/// möchte, anbietet. Es ist ein einer shared library, damit es am Server und am Client verwendet
	/// werden kann.
	/// Dafür wählt man im Server und im Client Dependencies und fügt die Library über Add Reference
	/// hinzu.
	/// Die Annotations sind wichtig, damit ASP.NET Core die Methode verarbeiten kann.
	/// </summary>
	[ServiceContract]
	public interface ICalcService
	{
		[OperationContract]
		int Add(int x, int y);
		[OperationContract]
		List<CalcStats> GetCalculationCount();
	}
}
