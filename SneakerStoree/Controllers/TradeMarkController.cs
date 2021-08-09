using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakerStoree.Entities;
using SneakerStoree.Models;
using SneakerStoree.Models.TradeMark;
using SneakerStoree.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Controllers
{

    [Authorize(Roles ="SystemAdmin")]
    public class TradeMarkController : Controller
    {
        private static string trademarkId;
        private static string trademarkName;
        private readonly ITradeMarkService tradeMarkService;

        public TradeMarkController(ITradeMarkService tradeMarkService)
        {
            this.tradeMarkService = tradeMarkService;
        }
        [Route("/TradeMark/Index/{pageNumber=1}/{pageSize=10}/{keyword=''}")]
        public async Task<IActionResult> Index(int? pageNumber, int? pageSize, string keyword)
        {
            var trademarks = await tradeMarkService.GetTradeMarks();
            var pagination = new Pagination(trademarks.Count, pageNumber, pageSize, null);
            keyword = keyword == "''" ? string.Empty : keyword;
            var tra = string.IsNullOrEmpty(keyword) ? trademarks : trademarks.Where(b => b.TradeMarkName.Contains(keyword));
           
            var traView = new TradeMarkViews()
            {
                TradeMarks = tra.Skip((pagination.CurrentPage - 1) * pagination.PageSize).Take(pagination.PageSize).ToList(),
                Pagination = pagination
            };
            return View(traView);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.TradeMarkName = trademarkName;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTradeMark createTradeMark)
        {
            if (ModelState.IsValid)
            {
                var newTradeMark = new TradeMark()
                {
                    TradeMarkName = createTradeMark.TradeMarkName,
                    IsDeleted = false
                };
                await tradeMarkService.Create(newTradeMark);
                return RedirectToAction("Index");
            }
            ViewBag.TradeMarkName = trademarkName;
            return View();
        }
        [HttpGet]
        [Route("/TradeMark/Modify/{traId}")]
        public async Task<IActionResult> Modify(int traId)
        {
            ViewBag.TradeMarkName = trademarkName;
            ViewBag.TradeMarkId = trademarkId;
            var trademark = await tradeMarkService.GetTradeMarkById(traId);
            var modifyTradeMark = new ModifyTradeMark()
            {
                TradeMarkId = trademark.TradeMarkId,
                TradeMarkName = trademark.TradeMarkName
            };
            return View(modifyTradeMark);
        }
        [HttpPost]
        public async Task<IActionResult> Modify(ModifyTradeMark modifytradeMark)
        {
            if (ModelState.IsValid)
            {
                var tradeMark = await tradeMarkService.GetTradeMarkById(modifytradeMark.TradeMarkId);
                if (tradeMark != null)
                {
                    tradeMark.TradeMarkId = modifytradeMark.TradeMarkId;
                    tradeMark.TradeMarkName = modifytradeMark.TradeMarkName;

                    await tradeMarkService.Modify(tradeMark);
                    return RedirectToAction("Index","TradeMark", new { tra = trademarkId});
                }
            }
            ViewBag.TradeMarkId = trademarkId;
            ViewBag.TradeMarkName = tradeMarkService;
            return View(modifytradeMark);
        }
        [HttpGet]
        [Route("/TradeMark/Remove/{traId}")]
        public async Task<IActionResult> Remove(int traId)
        {
            await tradeMarkService.Remove(traId);
            return RedirectToAction("Index", "TradeMark", new { traId = trademarkId });
        }
        [HttpGet]
        [Route("/TradeMark/Restore/{traId}")]
        public async Task<IActionResult> Restore(int traId)
        {
            await tradeMarkService.Restore(traId);
            return RedirectToAction("Index", "TradeMark", new { traId = trademarkId });
        }
    }
}
