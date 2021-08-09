using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Models.Cart
{
    public class SneakerItem
    {
        public int SneakerId { get; set; }
        public string SneakerName { get; set; }
        public int PublishYear { get; set; }
        public string Photo { get; set; }
        public int Size { get; set; }
        public string Information { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public int TradeMarkId { get; set; }
    }
}
