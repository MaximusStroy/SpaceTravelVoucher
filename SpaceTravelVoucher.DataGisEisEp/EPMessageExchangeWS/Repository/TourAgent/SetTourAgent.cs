using ConsoleApp3.EPMessageExchange;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Repository.TourAgent
{
    public class SetTourAgent
    {
        private string stringSetTourAgentRequest(string mnemonic, Models.TourAgent tourAgent, string human = "") =>
"<soapenv:Envelope xmlns:soapenv=" + '\u0022' + "http://schemas.xmlsoap.org/soap/envelope/" + '\u0022' +
" xmlns:mes=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange" + '\u0022' +
" xmlns:typ=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types" + '\u0022' +
" xmlns:bas=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/basic" + '\u0022' + ">" +
    "<soapenv:Header/>" +
    "<soapenv:Body>" +
        "<mes:SendRequest>" +
            "<typ:SendRequestRequest>" +
                "<typ:SenderProvidedRequestData>" +
                    "<typ:Sender>" +
                       $" <typ:Mnemonic>{mnemonic}</typ:Mnemonic>" +
                       $" <typ:HumanReadableName>{human}</typ:HumanReadableName>" +
                   " </typ:Sender>" +
                   " <bas:MessagePrimaryContent>" +
                       " <set:SetTourAgentsRequest xmlns:set=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/SetTourAgents" + '\u0022' + "> " +
                           " <set:TourAgents>" +
                               " <set:TourAgent>" +
                                    $"<set:name>{tourAgent.Name}</set:name>" +
                                    $"<set:inn>{tourAgent.Inn}</set:inn>" +
                                    $"<set:address>{tourAgent.Address}</set:address>" +
                                    $"<set:phoneNumber>{tourAgent.PhoneNumber}</set:phoneNumber>" +
                                    $"<set:email>{tourAgent.Email}</set:email>" +
                                    $"<set:active>{(tourAgent.Active == true ? 1 : 0)}</set:active>" +
                              "  </set:TourAgent>" +
                           " </set:TourAgents>" +
                      "  </set:SetTourAgentsRequest>" +
                  "  </bas:MessagePrimaryContent>" +
              "  </typ:SenderProvidedRequestData>" +
           " </typ:SendRequestRequest>" +
       " </mes:SendRequest>" +
   " </soapenv:Body>" +
"</soapenv:Envelope>";

        private string stringSetTourAgentResponse(string mnemonic, string id, string human = "") =>
"<soapenv:Envelope xmlns:soapenv=" + '\u0022' + "http://schemas.xmlsoap.org/soap/envelope/" + '\u0022' +
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

        private GetTourAgentsRequest SetPartnerRequest(Models.TourAgent tourAgent)
        {
            var request = new GetTourAgentsRequest();
            try
            {
                // создаем XML документ из строки
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(stringSetTourAgentRequest(EPMessageExchangeConfig.Mnemonic, tourAgent, "khame"));

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
                  //      Console.WriteLine("\nSetPartnerRequest:\n" + request.BodyRequest);
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
                    //Console.WriteLine(EPMessageExchangeConfig.FormatXml(text));
                    request.Error = EPMessageExchangeConfig.FormatXml(text);
                    return request;
                }
            }
        }

        public GetTourAgentsRequest SetPartnerResponse(Models.TourAgent partner)
        {
            var request = SetPartnerRequest(partner);
            try
            {
                // создаем XML документ из строки
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(stringSetTourAgentResponse(EPMessageExchangeConfig.Mnemonic, request.RequestId, "khame"));

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
                //Console.WriteLine($"\ns2:RequestId: {RequestID}");
                request.RequestId = RequestID;
                start = responseFromServer.IndexOf("<successMessage>");
                end = responseFromServer.IndexOf("</successMessage>");
                var Status = responseFromServer.Substring((start + "<successMessage>".Length), end - (start + "</successMessage>".Length - 1));
                //Console.WriteLine($"\nsuccessMessage: {Status}");
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
                    //Console.WriteLine(EPMessageExchangeConfig.FormatXml(text));
                    request.Error = EPMessageExchangeConfig.FormatXml(text);
                    return request;
                }
            }
        }
    }
}
