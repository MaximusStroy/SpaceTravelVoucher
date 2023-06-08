using Refit;
using SpaceTravelVoucher.DatabaseSpaceTravel.Models;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;

namespace SpaceTravelVoucher.Main.ApiInterface
{
    [Headers("x-api-key: 3840C65F221743DBB1FA61F14316EE93")]
    public interface IApiService
    {

        [Get("/VoucherXml")]
        public Task<IEnumerable<Api.ViewModels.VoucherXmlViewModels>> GetVouchers();

        [Post("/VoucherXml")]
        public Task InsertVoucher(CreateVoucherRequest model);


        [Get("/City")]
        public Task<IEnumerable<City>> GetCities();

        [Get("/City/{codeCountry}")]
        public Task<IEnumerable<City>> GetCities(string codeCountry);
    }
}
