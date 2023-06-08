using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceTravelVoucher.API.Models;

namespace SpaceTravelVoucher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly TESTSPACEContext _db;

        public CountryController(TESTSPACEContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await _db.Country.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                await _db.Logger.AddAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Level = "Error",
                    Message = ex.Message,
                    Point = "CountryController / Get"
                });
                await _db.SaveChangesAsync();
                return BadRequest();
            }
        }

        [HttpGet("codeCountry")]
        public async Task<IActionResult> Get(string codeCountry)
        {
            try
            {
                var list = await _db.Country.Where(x => x.Code == codeCountry).ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                await _db.Logger.AddAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Level = "Error",
                    Message = ex.Message,
                    Point = "CountryController / Get(string codeCountry)"
                });
                await _db.SaveChangesAsync();
                return BadRequest();
            }
        }
    }
}
