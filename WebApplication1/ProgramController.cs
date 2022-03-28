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
        private ShopDao _shopDao;
        public SzamlazoController()
        {
            _shopDao = ShopDao.GetInstance();
        }

        public IActionResult Index()
        {
            return View();
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
