namespace HaviSzamla
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.IO;

    
    public class SzamlazoController : Controller
    {
        private IShopDao _shopDao;
        public SzamlazoController(IShopDao shopDao)
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
