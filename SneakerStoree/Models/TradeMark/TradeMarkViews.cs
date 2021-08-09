using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Models.TradeMark
{
    public class TradeMarkViews
    {
        public List<Entities.TradeMark> TradeMarks { get; set; }
        public Pagination Pagination { get; set; }
    }
}
