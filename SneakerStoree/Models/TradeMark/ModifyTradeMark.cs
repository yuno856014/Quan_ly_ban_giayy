using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Models.TradeMark
{
    public class ModifyTradeMark
    {
        public int TradeMarkId { get; set; }
        [Required(ErrorMessage = "The Trademark name is required")]
        [MaxLength(500)]
        public string TradeMarkName { get; set; }
    }
}
