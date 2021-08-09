using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Models.Cart
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public SneakerItem SneakerItem { get; set; }
    }
}
 