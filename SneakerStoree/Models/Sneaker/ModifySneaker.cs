using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Models.Sneaker
{
    public class ModifySneaker
    {
        public int SneakerId { get; set; }
        [Required(ErrorMessage = "The Shoes name is required")]
        [MaxLength(500)]
        public string SneakerName { get; set; }
        [Required(ErrorMessage = "The publish year is required")]
        [Range(minimum: 1900, maximum: 2021)]
        public int PublishYear { get; set; }
        [Required]
        [Range(maximum: 42, minimum: 35)]
        public int Size { get; set; }
        [Required(ErrorMessage = "The Information is required")]
        [MaxLength(3000)]
        public string Information { get; set; }
        [Required(ErrorMessage = "The price is required")]
        public int Price { get; set; }
        [Required(ErrorMessage = "The Quantity is required")]
        public int Quantity { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int TradeMarkId { get; set; }
        public IFormFile Photo { get; set; }
        public string ExistPhoto { get; set; }
        public int TradeMarksId { get; set; }
    }
}
