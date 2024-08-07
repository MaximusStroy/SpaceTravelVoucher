﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SpaceTravelVoucher.DatabaseSpaceTravel.Models;

public partial class AviaCarrier
{
    public string Code { get; set; }

    public string CodeIata { get; set; }

    public string CodeRf { get; set; }

    public string CodeIcao { get; set; }

    public string CodeCalculate { get; set; }

    public string CodeCountry { get; set; }

    public string NameRu { get; set; }

    public string NameEn { get; set; }

    public string WebSite { get; set; }

    public string Status { get; set; }

    public string Remark { get; set; }

    public decimal? Version { get; set; }

    public int? Lowcoster { get; set; }

    public virtual Country CodeCountryNavigation { get; set; }

    public virtual StatusAviaCarrier StatusNavigation { get; set; }
}