﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SpaceTravelVoucher.DataSpaceTravel.Models
{
    public partial class City
    {
        public City()
        {
            RailwayStation = new HashSet<RailwayStation>();
            VoucherDestinationCityNavigation = new HashSet<Voucher>();
            VoucherLeavingCityNavigation = new HashSet<Voucher>();
        }

        public string Code { get; set; }
        public string Locode { get; set; }
        public string CodeCountry { get; set; }
        public string CodeRegionRf { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Timezone { get; set; }
        public string Remark { get; set; }
        public decimal? Version { get; set; }

        public virtual Country CodeCountryNavigation { get; set; }
        public virtual RegionRf CodeRegionRfNavigation { get; set; }
        public virtual ICollection<RailwayStation> RailwayStation { get; set; }
        public virtual ICollection<Voucher> VoucherDestinationCityNavigation { get; set; }
        public virtual ICollection<Voucher> VoucherLeavingCityNavigation { get; set; }
    }
}