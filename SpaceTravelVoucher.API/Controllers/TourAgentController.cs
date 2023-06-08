using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceTravelVoucher.API.Models;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Repository.TourAgent;

namespace SpaceTravelVoucher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourAgentController : ControllerBase
    {
        private readonly TESTSPACEContext _db;

        public TourAgentController(TESTSPACEContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await _db.TourAgency.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                await _db.Logger.AddAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Level = "Error",
                    Message = ex.Message,
                    Point = "TourAgentController / Get"
                });
                await _db.SaveChangesAsync();
                return BadRequest();
            }
        }

        [HttpGet("code")]
        public async Task<IActionResult> Get(int code)
        {
            try
            {
                var model = await _db.TourAgency.FirstOrDefaultAsync(x => x.Code == code);
                return Ok(model);
            }
            catch (Exception ex)
            {
                await _db.Logger.AddAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Level = "Error",
                    Message = ex.Message,
                    Point = "TourAgentController / Get(int code)"
                });
                await _db.SaveChangesAsync();
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(TourAgency model)
        {
            try
            {
                var setTourAgent = new SetTourAgent();
                setTourAgent.SetPartnerResponse(new TourAgent()
                {
                    Active = model.IsActive,
                    Address = model.Address,
                    Email = model.Mail,
                    Inn = model.Inn,
                    Name = model.NameRu,
                    PhoneNumber = model.NumberPhone
                });
                await _db.TourAgency.AddAsync(model);
                await _db.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                await _db.Logger.AddAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Level = "Error",
                    Message = ex.Message,
                    Point = "TourAgentController / Insert"
                });
                await _db.SaveChangesAsync();
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(TourAgency model)
        {
            try
            {
                var setTourAgent = new SetTourAgent();
                setTourAgent.SetPartnerResponse(new TourAgent()
                {
                    Active = model.IsActive,
                    Address = model.Address,
                    Email = model.Mail,
                    Inn = model.Inn,
                    Name = model.NameRu,
                    PhoneNumber = model.NumberPhone
                });
                _db.TourAgency.Update(model);
                await _db.SaveChangesAsync();
                return Ok(model);
            } catch (Exception ex)
            {
                await _db.Logger.AddAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Level = "Error",
                    Message = ex.Message,
                    Point = "TourAgentController / Update"
                });
                await _db.SaveChangesAsync();
                return BadRequest();
            }
        }
    }
}
