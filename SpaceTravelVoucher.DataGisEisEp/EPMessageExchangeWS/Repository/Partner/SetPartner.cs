using ConsoleApp3.EPMessageExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;

namespace SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Repository.Partner
{
    public class SetPartner
    {
        private string stringSetParnterRequest(string mnemonic, Models.Partner partner, string human = "") =>
@"<soapenv:Envelope xmlns:soapenv=" + '\u0022' + "http://schemas.xmlsoap.org/soap/envelope/" + '\u0022' +
" xmlns:mes=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange" + '\u0022' +
" xmlns:typ=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types" + '\u0022' +
" xmlns:bas=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/basic" + '\u0022' + ">" +
    "<soapenv:Header/>" +
    "<soapenv:Body>" +
        "<mes:SendRequest>" +
           " <typ:SendRequestRequest>" +
                "<typ:SenderProvidedRequestData>" +
                    "<typ:Sender>" +
                        $"<typ:Mnemonic>{mnemonic}</typ:Mnemonic>" +
                        $"<typ:HumanReadableName>{human}</typ:HumanReadableName>" +
                    "</typ:Sender>" +
                    "<bas:MessagePrimaryContent>" +
                        "<set:SetPartnersRequest xmlns:set=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/SetPartners" + '\u0022' + ">" +
                            "<set:Partners>" +
                                "<set:Partner>" +
                                    $"<set:code>{partner.Code}</set:code>" +
                                    $"<set:fullName>{partner.FullName}</set:fullName>" +
                                    $"<set:contacts>{partner.Contacts}</set:contacts>" +
                                    $"<set:active>{(partner.Active == true ? 1 : 0)}</set:active>" +
                                "</set:Partner>" +
                            "</set:Partners>" +
                       " </set:SetPartnersRequest>" +
                    "</bas:MessagePrimaryContent>" +
               " </typ:SenderProvidedRequestData>" +
            "</typ:SendRequestRequest>" +
        "</mes:SendRequest>" +
    "</soapenv:Body>" +
"</soapenv:Envelope>";

        private string stringSetPartnerResponse(string mnemonic, string id, string human = "") =>
@"<soapenv:Envelope xmlns:soapenv=" + '\u0022' + "http://schemas.xmlsoap.org/soap/envelope/" + '\u0022' +
" xmlns:mes=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange" + '\u0022' +
" xmlns:typ=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types" + '\u0022' +
" xmlns:bas=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/basic" + '\u0022' + ">" +
    "<soapenv:Header/>" +
    "<soapenv:Body>" +
        "<mes:GetResponse>" +
            "<typ:GetResponseRequest>" +
                "<typ:SenderProvidedGetResponseData>" +
                    "<typ:Sender>" +
                        $"<typ:Mnemonic>{mnemonic}</typ:Mnemonic>" +
                        $"<typ:HumanReadableName>{human}</typ:HumanReadableName>" +
                    "</typ:Sender>" +
                    "<bas:RequestReference>" +
                        $"<bas:RequestId>{id}</bas:RequestId>" +
                    "</bas:RequestReference>" +
                "</typ:SenderProvidedGetResponseData>" +
            "</typ:GetResponseRequest>" +
        "</mes:GetResponse>" +
    "</soapenv:Body>" +
"</soapenv:Envelope>";

        private GetPartnersRequest SetPartnerRequest(Models.Partner partner)
        {
            var request = new GetPartnersRequest();
            try
            {
                // создаем XML документ из строки
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(stringSetParnterRequest(EPMessageExchangeConfig.Mnemonic, partner, "khame"));

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
                        Console.WriteLine("\nSetPartnerRequest:\n" + request.BodyRequest);
                    }
                }
                var start = responseFromServer.IndexOf("<RequestId>");
                var end = responseFromServer.IndexOf("</RequestId>");
                string RequestID = responseFromServer.Substring((start + "<RequestId>".Length), end - (start + "</RequestId>".Length - 1));
                Console.WriteLine($"\nRequestID: {RequestID}");
                request.RequestId = RequestID;
                start = responseFromServer.IndexOf("<Status>");
                end = responseFromServer.IndexOf("</Status>");
                var Status = responseFromServer.Substring((start + "<Status>".Length), end - (start + "</Status>".Length - 1));
                Console.WriteLine($"\nStatus: {Status}");
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
                    Console.WriteLine(EPMessageExchangeConfig.FormatXml(text));
                    request.Error = EPMessageExchangeConfig.FormatXml(text);
                    return request;
                }
            }
        }

        public GetPartnersRequest SetPartnerResponse(Models.Partner partner)
        {
            var request = SetPartnerRequest(partner);
            try
            {
                // создаем XML документ из строки
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(stringSetPartnerResponse(EPMessageExchangeConfig.Mnemonic, request.RequestId, "khame"));

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
                        Console.WriteLine("\nSetPartnerResponse:\n" + request.BodyRequest);
                    }
                }
                var start = responseFromServer.IndexOf("<ns2:RequestId>");
                var end = responseFromServer.IndexOf("</ns2:RequestId>");
                string RequestID = responseFromServer.Substring((start + "<ns2:RequestId>".Length), end - (start + "</ns2:RequestId>".Length - 1));
                Console.WriteLine($"\ns2:RequestId: {RequestID}");
                request.RequestId = RequestID;
                start = responseFromServer.IndexOf("<successMessage>");
                end = responseFromServer.IndexOf("</successMessage>");
                var Status = responseFromServer.Substring((start + "<successMessage>".Length), end - (start + "</successMessage>".Length - 1));
                Console.WriteLine($"\nsuccessMessage: {Status}");
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
                    Console.WriteLine(EPMessageExchangeConfig.FormatXml(text));
                    request.Error = EPMessageExchangeConfig.FormatXml(text);
                    return request;
                }
            }
        }
    }
}
