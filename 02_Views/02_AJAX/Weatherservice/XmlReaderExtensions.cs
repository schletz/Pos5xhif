using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Weatherservice
{
    static class XmlReaderExtensions
    {
        public static decimal GetDecimal(this XmlReader reader, string attributeName, decimal defaultValue)
        {
            string strVal = reader.GetAttribute(attributeName);
            return decimal.TryParse(strVal, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal outVal) ? outVal : defaultValue;
        }
        public static decimal? GetDecimal(this XmlReader reader, string attributeName, decimal? defaultValue)
        {
            string strVal = reader.GetAttribute(attributeName);
            return decimal.TryParse(strVal, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal outVal) ? outVal : defaultValue;
        }
        public static int GetInt(this XmlReader reader, string attributeName, int defaultValue)
        {
            string strVal = reader.GetAttribute(attributeName);
            return int.TryParse(strVal, NumberStyles.Any, CultureInfo.InvariantCulture, out int outVal) ? outVal : defaultValue;
        }
        public static int? GetInt(this XmlReader reader, string attributeName, int? defaultValue)
        {
            string strVal = reader.GetAttribute(attributeName);
            return int.TryParse(strVal, NumberStyles.Any, CultureInfo.InvariantCulture, out int outVal) ? outVal : defaultValue;
        }
        public static DateTime GetDateTime(this XmlReader reader, string attributeName, DateTime defaultValue)
        {
            string strVal = reader.GetAttribute(attributeName);
            return DateTime.TryParse(
                strVal, 
                System.Globalization.CultureInfo.InvariantCulture, 
                System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime outVal) ? outVal : defaultValue;
        }
        public static DateTime? GetDateTime(this XmlReader reader, string attributeName, DateTime? defaultValue)
        {
            string strVal = reader.GetAttribute(attributeName);
            return DateTime.TryParse(
                strVal,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime outVal) ? outVal : defaultValue;
        }
    }
}
