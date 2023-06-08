using Newtonsoft.Json;
using SpaceTravelVoucher.Api.ViewModels;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;
using SpaceTravelVoucher.Main.ViewModels;
using System.Reflection;

namespace SpaceTravelVoucher.Main.Services
{
    public static class Helper
    {
        private static int NotNullFields(object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public |
                                          BindingFlags.NonPublic |
                                          BindingFlags.Instance);
            var count = 0;
            foreach (var item in fields)
                if (item.GetValue(obj) != null)
                    count++;

            return count;
        }

        public static CreateOrEditVoucherViewModel GetVoucher(VoucherXmlViewModels model)
        {
            CreateOrEditVoucherViewModel obj = new CreateOrEditVoucherViewModel();
            obj.CreateVoucherRequest = model.CreateVoucherRequest;
            obj.CodeAuth = model.CodeAuth;
            var buff = model.CreateVoucherRequest.Voucher.Order.Customer.Item;

            obj.CompanyCustomer = JsonConvert.DeserializeObject<CreateVoucherRequestVoucherOrderCustomerCompanyCustomer>(buff.ToString());
            obj.ProprietorCustomer = JsonConvert.DeserializeObject<soleProprietorCustomerType>(buff.ToString());
            obj.IndividualCustomer = JsonConvert.DeserializeObject<individualCustomerType>(buff.ToString());

            if (NotNullFields(obj.CompanyCustomer) > NotNullFields(obj.ProprietorCustomer) &&
                NotNullFields(obj.CompanyCustomer) > NotNullFields(obj.IndividualCustomer))
            {
                obj.ProprietorCustomer = null; obj.IndividualCustomer = null;
            }
            else if (NotNullFields(obj.ProprietorCustomer) > NotNullFields(obj.CompanyCustomer) &&
                NotNullFields(obj.ProprietorCustomer) > NotNullFields(obj.IndividualCustomer))
            {
                obj.CompanyCustomer = null; obj.IndividualCustomer = null;
            }
            else if (NotNullFields(obj.IndividualCustomer) > NotNullFields(obj.ProprietorCustomer) &&
                NotNullFields(obj.IndividualCustomer) > NotNullFields(obj.CompanyCustomer))
            {
                obj.ProprietorCustomer = null; obj.CompanyCustomer = null;
            }

            return obj;
        } 
    }
}
