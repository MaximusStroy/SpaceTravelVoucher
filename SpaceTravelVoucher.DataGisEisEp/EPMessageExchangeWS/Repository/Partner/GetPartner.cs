using ConsoleApp3.EPMessageExchange;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Repository.Partner
{
    public class GetPartner
    {
        private string stringGetParnterRequest(string mnemonic, string human = "") =>
            @"<soapenv:Envelope xmlns:soapenv=" + '\u0022' + "http://schemas.xmlsoap.org/soap/envelope/" + '\u0022' +
               " xmlns:mes=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange" + '\u0022' +
               " xmlns:typ=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types" + '\u0022' +
               " xmlns:bas=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/basic" + '\u0022' + ">" +
               " <soapenv:Header/>" +
               " <soapenv:Body>" +
               " <mes:SendRequest>" +
                   " <typ:SendRequestRequest>" +
                       " <typ:SenderProvidedRequestData>" +
                               " <typ:Sender>" +
                                   $" <typ:Mnemonic>{mnemonic}</typ:Mnemonic>" +
                                   $" <typ:HumanReadableName>{human}</typ:HumanReadableName>" +
                               " </typ:Sender>" +
                               " <bas:MessagePrimaryContent>" +
                                    "<ns6:GetPartnersRequest xmlns:ns6=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/GetPartners" + '\u0022' + "/>" +
                               " </bas:MessagePrimaryContent>" +
                           " </typ:SenderProvidedRequestData>" +
                        "</typ:SendRequestRequest>" +
                    "</mes:SendRequest>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope>";

        private string stringGetPartnerResponse(string mnemonic, string human = "", string id = "")
        {
            string str =
            @"<soapenv:Envelope xmlns:soapenv=" + '\u0022' + "http://schemas.xmlsoap.org/soap/envelope/" + '\u0022' +
               " xmlns:mes=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange" + '\u0022' +
               " xmlns:typ=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types" + '\u0022' +
               " xmlns:bas=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/basic" + '\u0022' + ">" +
               " <soapenv:Header/>" +
               " <soapenv:Body>" +
               " <mes:GetResponse>" +
                   " <typ:GetResponseRequest>" +
                       " <typ:SenderProvidedGetResponseData>" +
                               " <typ:Sender>" +
                                   $" <typ:Mnemonic>{mnemonic}</typ:Mnemonic>" +
                                   $" <typ:HumanReadableName>{human}</typ:HumanReadableName>" +
                               " </typ:Sender>" +
                                    "<bas:RequestReference>" +
                                        $"<bas:RequestId>{id}</bas:RequestId>" +
                                    "</bas:RequestReference>" +
                           " </typ:SenderProvidedGetResponseData>" +
                        "</typ:GetResponseRequest>" +
                    "</mes:GetResponse>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope>";
            return str;
        }


        public string One = "";

        private GetPartnersRequest GetPartnerRequest()
        {
            var request = new GetPartnersRequest();
            try
            {
                // создаем XML документ из строки
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(stringGetParnterRequest(EPMessageExchangeConfig.Mnemonic, "khame"));

                // создаем HTTP запрос
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(EPMessageExchangeConfig.Url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "text/xml;charset=UTF-8";
                string responseFromServer = "";
                // отправляем данные в запросе
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(requestStream);
                }

                // получаем ответ от сервера
                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream);
                        responseFromServer = reader.ReadToEnd();
                        request.BodyRequest = EPMessageExchangeConfig.FormatXml(responseFromServer);
                        //Console.WriteLine("\nGetPartnerRequest:\n" + request.BodyRequest);
                    }
                }
                var start = responseFromServer.IndexOf("<RequestId>");
                var end = responseFromServer.IndexOf("</RequestId>");
                string RequestID = responseFromServer.Substring((start + "<RequestId>".Length), end - (start + "</RequestId>".Length - 1));
                //Console.WriteLine($"\nRequestID: {RequestID}");
                request.RequestId = RequestID;
                start = responseFromServer.IndexOf("<Status>");
                end = responseFromServer.IndexOf("</Status>");
                var Status = responseFromServer.Substring((start + "<Status>".Length), end - (start + "</Status>".Length - 1));
                //Console.WriteLine($"\nStatus: {Status}");
                request.Status = Status;
                return request;

            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    // Console.WriteLine(EPMessageExchangeConfig.FormatXml(text));
                    request.Error = EPMessageExchangeConfig.FormatXml(text);
                    return request;
                }
            }
        }

        private GetPartnersRequest GetPartnerResponse()
        {
            var request = GetPartnerRequest();
            //var request = new GetPartnersRequest();
            try
            {
                Thread.Sleep(2000);
                // создаем XML документ из строки
                XmlDocument soapEnvelopeXml = new XmlDocument();
                // request.RequestId = "94aa5f10-fe22-11ed-a245-d92da5ca0eb8";
                soapEnvelopeXml.LoadXml(stringGetPartnerResponse(mnemonic: EPMessageExchangeConfig.Mnemonic, id: request.RequestId));

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(EPMessageExchangeConfig.Url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "text/xml;charset=UTF-8";
                string responseFromServer = "";

                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(requestStream);
                }

                using (var response = httpWebRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream);

                        responseFromServer = reader.ReadToEnd();
                        request.BodyRequest = EPMessageExchangeConfig.FormatXml(responseFromServer);
                        // Console.WriteLine("GetPartnerResponse:\n" + request.BodyRequest);
                    }
                }
                var start = responseFromServer.IndexOf("<partnersData>");
                var end = responseFromServer.IndexOf("</partnersData>");
                string partnersData = responseFromServer.Substring((start + "<partnersData>".Length), end - (start + "</partnersData>".Length - 1));
                //Console.WriteLine($"\nPartnersData: {partnersData}\n");
                request.PartnersData = partnersData;
                return request;
            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    //Console.WriteLine("\n GetPartnerResponse:\n" + EPMessageExchangeConfig.FormatXml(text));
                    request.Error = EPMessageExchangeConfig.FormatXml(text);
                    return request;
                }
            }
        }

        public IEnumerable<Models.Partner> GetPartners()
        {
            var request = GetPartnerResponse();
            var arrBytes = request.PartnersData;
            var list = new List<Models.Partner>();
            try
            {
                if (!string.IsNullOrEmpty(arrBytes))
                    using (MemoryStream arrStream = new MemoryStream(Convert.FromBase64String(arrBytes)))
                    {
                        using (ZipArchive archive = new ZipArchive(arrStream, ZipArchiveMode.Read))
                        {
                            DateTime now = DateTime.Now;
                            var file = $"{now.ToString("dd_MM_yyyy_hh_mm_ss")}.xml";
                            //archive.ExtractToDirectory(file);
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                entry.ExtractToFile(file);
                                //Helper<Partners>.LoadFromExcel(file);
                                Console.WriteLine($" / {entry.Name}");

                                var test = new Models.Partners();

                                XmlSerializer serializer = new XmlSerializer(typeof(Models.Partners));
                                using (FileStream reader = new FileStream(file, FileMode.Open))
                                {
                                    test = (Models.Partners)serializer.Deserialize(reader);
                                }
                                System.IO.FileInfo di = new FileInfo(file);
                                di.Delete();
                                return test.Partner;

                            }
                        }
                    }
                else Console.WriteLine("Данных (base64) нет");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.InnerException.Message);
            }
            return null;
        }
    }
}
