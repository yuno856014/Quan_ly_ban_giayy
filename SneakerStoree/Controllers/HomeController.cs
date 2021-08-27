using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SneakerStoree.Models.Cart;
using SneakerStoree.Models.Home;
using SneakerStoree.Models.Sneaker;
using SneakerStoree.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITradeMarkService tradeMarkService;
        private readonly ISneakerService sneakerService;
        public const string CARTKEY = "SNSTORE";

        public HomeController(ITradeMarkService tradeMarkService, ISneakerService sneakerService)
        {
            this.tradeMarkService = tradeMarkService;
            this.sneakerService = sneakerService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await tradeMarkService.GetTradeMarks());
        }
        [Route("/Home/AddToCart/{sneakerId:int}", Name = "Index")]
        public async Task<IActionResult> AddToCart(int sneakerId)
        {
            var sneaker = await sneakerService.GetSneakerById(sneakerId);
            if (sneaker == null)
            {
                return NotFound("No produts");
            }
            var sneakerItem = new SneakerItem()
            {
                SneakerId = sneaker.SneakerId,
                SneakerName = sneaker.SneakerName,
                Information = sneaker.Information,
                Photo = sneaker.Photo,
                Price = sneaker.Price,
                PublishYear = sneaker.PublishYear,
                Quantity = sneaker.Quantity,
                Size = sneaker.Size
            };
            var cart = GetCartItems();
            var cartitem = cart.Find(s => s.SneakerItem.SneakerId == sneakerId);
            if (cartitem != null)
            {
                cartitem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem() { Quantity = 1, SneakerItem = sneakerItem });
            }

            SaveCartSession(cart);
            return RedirectToAction("Cart");
        }
        [Route("Home/Cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }
        [Route("Home/UpdateCart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int sneakerId, [FromForm] int quantity)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(s => s.SneakerItem.SneakerId == sneakerId);
            if (cartitem != null)
            {
                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);
            return Ok();
        }
        [Route("/Home/RemoveCart/{sneakerId:int}")]
        public IActionResult RemoveCart([FromRoute] int sneakerId)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.SneakerItem.SneakerId == sneakerId);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }
        [HttpGet("/Home/View/{sneakerId}")]
        public async Task<IActionResult> View(int sneakerId)
        {
            var sneaker = await sneakerService.GetSneakerById(sneakerId);
            var viewSneaker = new ViewSneakerHome()
            {
                SneakerId = sneaker.SneakerId,
                SneakerName = sneaker.SneakerName,
                ExistPhoto = sneaker.Photo,
                Price = sneaker.Price,
                Information = sneaker.Information,
                PublishYear = sneaker.PublishYear,
                Quantity = sneaker.Quantity,
                Size = sneaker.Size,
                TradeMarkId = sneaker.TradeMarkId,
                TradeMark = sneaker.TradeMarks
            };
            return View(viewSneaker);
        }
    }
}
