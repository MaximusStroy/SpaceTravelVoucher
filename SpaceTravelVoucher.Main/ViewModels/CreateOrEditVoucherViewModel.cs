using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;

namespace SpaceTravelVoucher.Main.ViewModels
{
    public class CreateOrEditVoucherViewModel
    {
        public CreateVoucherRequest CreateVoucherRequest { get; set; }
        public CreateVoucherRequestVoucherOrderCustomerCompanyCustomer CompanyCustomer { get; set; }
        public soleProprietorCustomerType ProprietorCustomer { get; set; }
        public individualCustomerType IndividualCustomer { get; set; }

        public string CodeAuth { get; set; }
    }
}
