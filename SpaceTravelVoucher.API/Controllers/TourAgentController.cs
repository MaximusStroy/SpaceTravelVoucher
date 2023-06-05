using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceTravelVoucher.DataSpaceTravel.Models;
using SpaceTravelVoucher.DataSpaceTravel.Repository;

namespace SpaceTravelVoucher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourAgentController : ControllerBase
    {
        private DataRepository<TourAgency, string> _repository;

        public TourAgentController()
        {
            _repository = new DataRepository<TourAgency, string>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetMany());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetOne(string code)
        {
            var obj = await _repository.GetOne(code);
            if (obj == null) return BadRequest();
            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(TourAgency model)
        {
            return Ok(await _repository.Insert(model));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            var obj = await _repository.GetOne(code);
            if (obj == null) return BadRequest();
            return Ok(await _repository.Delete(obj));
        }
    }
}
