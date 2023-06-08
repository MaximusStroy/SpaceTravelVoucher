using Mapster;
using Refit;
using SpaceTravelVoucher.Api.ViewModels;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;
using SpaceTravelVoucher.Main.ApiInterface;
using SpaceTravelVoucher.Main.ViewModels;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;

namespace SpaceTravelVoucher.Main.Repository.Voucher
{

    public class VoucherRepository
    {
        private readonly IApiService _voucherService;
        public VoucherRepository()
        {
            _voucherService = RestService.For<IApiService>("https://localhost:5001/api/");
        }

        public async Task<IEnumerable<ShortVoucherViewModel>> GetShort()
        {
            var vouchers = await _voucherService.GetVouchers();
            var config = new TypeAdapterConfig();

            config.NewConfig<VoucherXmlViewModels, ShortVoucherViewModel>()
                .Map(dest => dest.Status, src => src.CreateVoucherRequest.Voucher.status)
                .Map(dest => dest.Comment, src => src.CreateVoucherRequest.Voucher.comment)
                .Map(dest => dest.Number, src => src.CreateVoucherRequest.Voucher.number)
                .Map(dest => dest.TripStartDate, src => src.CreateVoucherRequest.Voucher.tripStartDate)
                .Map(dest => dest.TripEndDate, src => src.CreateVoucherRequest.Voucher.tripEndDate)
                .IgnoreNonMapped(true);

            var response = vouchers.Adapt<IEnumerable<ShortVoucherViewModel>>(config);

            return response;
        }

        public async Task<IEnumerable<VoucherXmlViewModels>> GetMany()
        {
            var vouchers = await _voucherService.GetVouchers();
            return vouchers;
        }

        public async Task<IEnumerable<VoucherXmlViewModels>> GetManyByCode(string number, string codeAuth)
        {
            var vouchers = await _voucherService.GetVouchers();
            return vouchers;
        }

        public async Task InsertVoucher(CreateVoucherRequest model)
        {
            await _voucherService.InsertVoucher(model);
        }

    }
}
