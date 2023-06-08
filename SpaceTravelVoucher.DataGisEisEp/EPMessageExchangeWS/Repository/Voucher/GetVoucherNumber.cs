using ConsoleApp3.EPMessageExchange;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Repository.Voucher
{
    public class GetVoucherNumber
    {
        private string createVoucherNumberRequest(string mnemonic, string human = "") =>
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
                        $"<typ:Mnemonic>{mnemonic}</typ:Mnemonic>" +
                        $"<typ:HumanReadableName>{human}</typ:HumanReadableName>" +
                    "</typ:Sender>" +
                    "<bas:MessagePrimaryContent>" +
                        "<ns6:CreateVoucherNumberRequest xmlns:ns6=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/CreateVoucherNumber" + '\u0022' + "/>" +
                    "</bas:MessagePrimaryContent>" +
                "</typ:SenderProvidedRequestData>" +
            "</typ:SendRequestRequest>" +
        "</mes:SendRequest>" +
    "</soapenv:Body>" +
"</soapenv:Envelope>";

        private string createVoucherNumberResponse(string mnemonic, string id, string human = "") =>
"<soapenv:Envelope xmlns:soapenv=" + '\u0022' + "http://schemas.xmlsoap.org/soap/envelope/" + '\u0022' +
" xmlns:mes=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange" + '\u0022' +
" xmlns:typ=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types" + '\u0022' +
" xmlns:bas=" + '\u0022' + "urn://artefacts-russiatourism-ru/services/message-exchange/types/basic" + '\u0022' + ">" +
    "<soapenv:Header/>" +
    "<soapenv:Body>" +
        "<mes:GetResponse>" +
    "<typ:GetResponseRequest>" +
        "<typ:SenderProvidedGetResponseData Id = " + '\u0022' + "1" + '\u0022' + ">" +
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


        private GetPartnersRequest GetVoucherNumberRequest()
        {
            var request = new GetPartnersRequest();
            try
            {
                // создаем XML документ из строки
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(createVoucherNumberRequest(EPMessageExchangeConfig.Mnemonic, "khame"));

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

        public CreateVoucherNumberResponse GetVoucherNumberResponse()
        {
            var request = GetVoucherNumberRequest();
            var voucherNumber = new CreateVoucherNumberResponse();
            //var request = new GetPartnersRequest();
            try
            {
                Thread.Sleep(2000);
                // создаем XML документ из строки
                XmlDocument soapEnvelopeXml = new XmlDocument();
                // request.RequestId = "94aa5f10-fe22-11ed-a245-d92da5ca0eb8";
                soapEnvelopeXml.LoadXml(createVoucherNumberResponse(mnemonic: EPMessageExchangeConfig.Mnemonic, id: request.RequestId));

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
                var start = responseFromServer.IndexOf("<voucherNumber>");
                var end = responseFromServer.IndexOf("</voucherNumber>");
                voucherNumber.voucherNumber = responseFromServer.Substring((start + "<voucherNumber>".Length), end - (start + "</voucherNumber>".Length - 1)); 
                start = responseFromServer.IndexOf("<voucherNumberCreatedDateTime>");
                end = responseFromServer.IndexOf("</voucherNumberCreatedDateTime>");
                voucherNumber.voucherNumberCreatedDateTime = DateTime.Parse(responseFromServer.Substring((start + "<voucherNumberCreatedDateTime>".Length), end - (start + "</voucherNumberCreatedDateTime>".Length - 1)));
                //Console.WriteLine($"\nPartnersData: {partnersData}\n");

                return voucherNumber;
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
                }
                return null;
            }
        }

    }
}
