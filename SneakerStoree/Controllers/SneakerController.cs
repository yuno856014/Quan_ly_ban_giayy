using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SneakerStoree.Entities;
using SneakerStoree.Models;
using SneakerStoree.Models.Sneaker;
using SneakerStoree.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Controllers
{
    public class SneakerController : Controller
    {
        private static int tradeMarkId;
        private static string tradeMarkName;
        private readonly ITradeMarkService tradeMarkService;
        private readonly ISneakerService sneakerService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SneakerController(ITradeMarkService tradeMarkService,
                                ISneakerService sneakerService,
                                IWebHostEnvironment webHostEnvironment)
        {   
            this.tradeMarkService = tradeMarkService;
            this.sneakerService = sneakerService;
            this.webHostEnvironment = webHostEnvironment;
        }
        //[Route("/Sneaker/Index/{traId=1}/{pageNumber=1}/{pageSize=10}/{keyword=''}")]

        public async Task<IActionResult> Index(int traId,int? pageNumber, int? pageSize,string keyword )
        {
            tradeMarkId = traId;
            var trademark = await tradeMarkService.GetTradeMarkById(traId);
            tradeMarkName = trademark.TradeMarkName;
            var pagination = new Pagination(trademark.Sneakers.Count, pageNumber, pageSize, keyword);
            keyword = keyword == "''" ? string.Empty : keyword;
            var sneaker = string.IsNullOrEmpty(keyword) ? trademark.Sneakers : trademark.Sneakers.Where(s => s.SneakerName.Contains(keyword)).ToList();
            sneaker = sneaker.OrderByDescending(s => s.SneakerId).ToList().Skip((pagination.CurrentPage - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
            trademark.Sneakers = sneaker;
            var listSneaker = new ListSneakercs()
            {
                TradeMark = trademark,
                Pagination = pagination
            };
            return View(listSneaker);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.TradeMarkName = tradeMarkName;
            ViewBag.TradeMarkId = tradeMarkId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSneaker createSneaker)
        {
            if (ModelState.IsValid)
            {
                string filename = "no-photo.jpg";
                if (createSneaker.Photo != null)
                {
                    string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\images\");
                    filename = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{createSneaker.Photo.FileName}";
                    string fullpath = Path.Combine(folderPath, filename);
                    using (var file = new FileStream(fullpath, FileMode.Create))
                    {
                        createSneaker.Photo.CopyTo(file);
                    }
                }
                var newSneaker = new Sneaker()
                {
                    Photo = $"/images/{filename}",
                    Quantity = createSneaker.Quantity,
                    Information = createSneaker.Information,
                    Price = createSneaker.Price,
                    PublishYear = createSneaker.PublishYear,
                    TradeMarkId = tradeMarkId,
                    Size = createSneaker.Size,
                    SneakerName = createSneaker.SneakerName,
                    IsDeleted = false
                };
                await sneakerService.Create(newSneaker);
                return RedirectToAction("Index", "Sneaker", new { traId = tradeMarkId });
            }
            ViewBag.TradeMarkName = tradeMarkName;
            ViewBag.TradeMarkId = tradeMarkId;
            return View();
        }
        [HttpGet("/Sneaker/Modify/{sneakerId}")]
        public async Task<IActionResult> Modify(int sneakerId)
        {
            ViewBag.TradeMarkName = tradeMarkName;
            ViewBag.TradeMarkId = tradeMarkId;
            var sneaker = await sneakerService.GetSneakerById(sneakerId);
            var modifySneaker = new ModifySneaker()
            {
                SneakerId = sneaker.SneakerId,
                ExistPhoto = sneaker.Photo,
                Information = sneaker.Information,
                PublishYear = sneaker.PublishYear,
                Quantity = sneaker.Quantity,
                Price = sneaker.Price,
                Size = sneaker.Size,
                SneakerName = sneaker.SneakerName,
                TradeMarkId = sneaker.TradeMarkId
            };
            return View(modifySneaker);
        }
        [HttpPost]
        public async Task<IActionResult> Modify(ModifySneaker modifySneaker)
        {
            if (ModelState.IsValid)
            {
                var sneaker = await sneakerService.GetSneakerById(modifySneaker.SneakerId);
                if (sneaker != null)
                {
                    string filename = sneaker.Photo;
                    if (modifySneaker.Photo != null)
                    {
                        //Delete old photo
                        var oldFileName = filename.Split("/")[2];
                        if (string.Compare(oldFileName, "no-photo.jpg") != 0)
                        {
                            System.IO.File.Delete(Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\images\", oldFileName));

                        }
                        string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\images\");
                        filename = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{modifySneaker.Photo.FileName}";
                        string fullpath = Path.Combine(folderPath, filename);
                        using (var file = new FileStream(fullpath, FileMode.Create))
                        {
                            modifySneaker.Photo.CopyTo(file);
                        }
                    }
                    sneaker.Photo = modifySneaker.Photo != null ? $"/images/{filename}" : filename;
                    sneaker.SneakerId = modifySneaker.SneakerId;
                    sneaker.Information = modifySneaker.Information;
                    sneaker.Price = modifySneaker.Price;
                    sneaker.PublishYear = modifySneaker.PublishYear;
                    sneaker.Size = modifySneaker.Size;
                    sneaker.SneakerName = modifySneaker.SneakerName;
                    sneaker.TradeMarkId = tradeMarkId;


                    await sneakerService.Modify(sneaker);
                    return RedirectToAction("Index", "Sneaker", new { traid = tradeMarkId });
                }
            }
            ViewBag.TradeMarkName = tradeMarkName;
            ViewBag.TradeMarkId = tradeMarkId;
            return View(modifySneaker);
        }
        [HttpGet("/Sneaker/View/{sneakerId}")]
        public async Task<IActionResult> View(int sneakerId)
        {
            var sneaker = await sneakerService.GetSneakerById(sneakerId);
            var viewSneaker = new ViewSneaker()
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

        [HttpGet("/Sneaker/Remove/{sneakerId}")]
        public async Task<IActionResult> Remove(int sneakerId)
        {
            await sneakerService.Remove(sneakerId);
            return RedirectToAction("Index", "Sneaker", new { traId = tradeMarkId });

        }
        [HttpGet("/Sneaker/Restore/{sneakerId}")]
        public async Task<IActionResult> Restore(int sneakerId)
        {
            await sneakerService.Restore(sneakerId);
            return RedirectToAction("Index", "Sneaker", new { traId = tradeMarkId });
        }
    }
}
