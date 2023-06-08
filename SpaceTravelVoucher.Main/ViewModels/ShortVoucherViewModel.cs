using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;

namespace SpaceTravelVoucher.Main.ViewModels
{
    public class ShortVoucherViewModel
    {
        public string Number { get; set; }

        public DateTime TripStartDate { get; set; }

        public DateTime TripEndDate { get; set; }

        public string Comment { get; set; }
        public voucherStatus Status { get; set; }
    }
}
