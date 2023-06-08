using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp3.EPMessageExchange
{
    public static class EPMessageExchangeConfig
    {
        public static string Mnemonic { get; set; } = "1167746511162-2";

        public static string Url { get; set; } = "http://localhost:5556/EPMessageExchangeWS?wsdl";
        public static string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }
    }
}

