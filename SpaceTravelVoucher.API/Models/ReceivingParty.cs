﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SpaceTravelVoucher.API.Models;

public partial class ReceivingParty
{
    public int Code { get; set; }

    public string CodeGis { get; set; }

    public string NameRu { get; set; }

    public string NameEn { get; set; }

    public string NumberPhone { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Voucher> Voucher { get; set; } = new List<Voucher>();
}