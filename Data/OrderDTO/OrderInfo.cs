using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OrderDTO
{
    public class OrderInfo
    {
        public string Status { get; set; }
        public string Shipper { get; set; }
        public string TrackingCode { get; set; }
    }

    public class OrderDetailInfo
    {
        public int OrderId { get; set; }
        public string Shipper { get; set; }
        public string Status { get; set; }
        public string TrackingCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
