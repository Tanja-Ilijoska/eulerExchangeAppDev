using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace EulerExchangeAppDev.DataAccess
{
    public class CurrencyRate
    {
        public static String getCurrencyRates()
        {
            XmlDocument xDoc = new XmlDocument();
            try { 
                xDoc.Load("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
            }
            catch(Exception)
            {
                return "";
            }

            XmlNodeList xNodeList = xDoc.DocumentElement.LastChild.FirstChild.ChildNodes;

            String result = "";
            foreach (XmlNode xNode in xNodeList)
            {
                if (xNode.Name == "Cube")
                {
                    string rate = xNode.Attributes["rate"].Value;
                    string currency = xNode.Attributes["currency"].Value;

                    result += "EUR/" + currency + ": " + rate + ", ";
                }
            }

            return result;
        }
    }
}