﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SpaceTravelVoucher.API.Models;

public partial class RailCarrier
{
    public string Code { get; set; }

    public string CodeCountry { get; set; }

    public string ShortNameRu { get; set; }

    public string ShortNameEn { get; set; }

    public string NameRu { get; set; }

    public string NameEn { get; set; }

    public short Status { get; set; }

    public string WebSite { get; set; }

    public string Remark { get; set; }

    public decimal? Version { get; set; }

    public virtual Country CodeCountryNavigation { get; set; }

    public virtual StatusRailCarrier StatusNavigation { get; set; }
}