using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;

namespace SpaceTravelVoucher.Main.ViewModels
{
    public class VoucherViewModel
    {
        public string Number { get; set; }

        public voucherStatus Status { get; set; }

        public TourAgent TourAgent { get; set; }

        public DateTime TripStartDate { get; set; }

        public DateTime TripEndDate { get; set; }

        public string Comment { get; set; }

        public VoucherTypeViewModel VoucherType { get; set; }

        public CreateVoucherRequestVoucherOrder Order { get; set; }

        public partnerType Partner { get; set; }

        public CityViewModel LeavingCity { get; set; }

        public CityViewModel DestinationCity { get; set; }

        public CountryViewModel LeavingCountry { get; set; }

        public CountryViewModel DestinationCountry { get; set; }

        public List<travelerType> Travelers { get; set; }
    }


}
