﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("Order")]
public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }
    public string? Shipper { get; set; }

    public string? TrackingCode { get; set; }

    public string? PaymentMethod { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
