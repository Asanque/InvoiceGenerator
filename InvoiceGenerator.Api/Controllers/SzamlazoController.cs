using InvoiceGenerator.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceGenerator.Api.Controllers
{
    public class SzamlazoController : Controller
    {
        private IShopRepository _shopDao;
        public SzamlazoController(IShopRepository shopDao)
        {
            _shopDao = shopDao;
        }

        public IActionResult Index()
        {
            var shopIds = _shopDao.GetShopIds();
            return View(shopIds);
        }

        [Route("/shop/{id}")]
        public IActionResult ShopDetails(int id)
        {
            var data = _shopDao.GetShop(id);
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }
            
    }
}
