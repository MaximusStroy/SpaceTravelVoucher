using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceTravelVoucher.Api.ViewModels;
using SpaceTravelVoucher.API.Models;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Repository.Voucher;
using Swashbuckle.Swagger;
using System.Xml;
using System.Xml.Serialization;

namespace SpaceTravelVoucher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherXmlController : ControllerBase
    {
        private readonly TESTSPACEContext _db;

        public VoucherXmlController(TESTSPACEContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await _db.VoucherXml.ToListAsync();
                List<VoucherXmlViewModels> vouchers = new List<VoucherXmlViewModels>();
                foreach (var item in list)
                {
                    var startIndex = item.XmlVoucher.IndexOf("<CreateVoucherRequest");
                    var endIndex = item.XmlVoucher.IndexOf("</CreateVoucherRequest>");
                    var voucherStr = item.XmlVoucher.Substring(startIndex, endIndex - (startIndex + "</CreateVoucherRequest>".Length - 1));
                    voucherStr += "</CreateVoucherRequest>";
                    var voucher = new VoucherXmlViewModels();
                    voucher.CreateVoucherRequest = MySerializer<CreateVoucherRequest>.Deserialize(voucherStr);
                    voucher.CodeAuth = item.CodeAuth;
                    vouchers.Add(voucher);
                }

                return Ok(vouchers);
            }
            catch (Exception ex)
            {
                await _db.Logger.AddAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Level = "Error",
                    Message = ex.Message,
                    Point = "VoucherXmlController / Get"
                });
                await _db.SaveChangesAsync();
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertVoucher(CreateVoucherRequest model)
        {
            try
            {
                CreateVoucherRequest voucherRequest = new CreateVoucherRequest();
                voucherRequest.Voucher = model.Voucher;

                var getVoucherNumber = new GetVoucherNumber();
                var number = getVoucherNumber.GetVoucherNumberResponse();

                voucherRequest.Voucher.number = number.voucherNumber;
                var setVoucher = new SetVoucher();
                var voucherStr = MySerializer<CreateVoucherRequest>.Serialize(voucherRequest);
                var codeAuth = setVoucher.SetVoucherResponse(voucherStr);

                await _db.VoucherXml.AddAsync(new VoucherXml()
                {
                    XmlVoucher = codeAuth.BodyRequest,
                    CodeAuth = codeAuth.voucherAuthorizationCode
                });
                await _db.SaveChangesAsync();
                return Ok(voucherStr);
            }
            catch (Exception ex)
            {
                await _db.Logger.AddAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Level = "Error",
                    Message = ex.Message,
                    Point = "VoucherXmlController / InsertVoucher(CreateVoucherRequest model)"
                });
                await _db.SaveChangesAsync();
                return BadRequest();
            }
        }
    }

    public class MySerializer<T> where T : class
    {
        public static T Deserialize(string serializedResults)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(serializedResults))
            return (T)serializer.Deserialize(stringReader);
        }

        public static string Serialize(T obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            using (var sww = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
                {
                    xsSubmit.Serialize(writer, obj);
                    return sww.ToString();
                }
            }
        }
    }
}
