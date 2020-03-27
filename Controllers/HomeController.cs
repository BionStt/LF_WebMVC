using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogRepository _catalogRepository;

        public HomeController(ILogger<HomeController> logger, ICatalogRepository catalogRepository)
        {
            _logger = logger;
            _catalogRepository = catalogRepository;
        }

        /*
        public IActionResult Index()
        {
            return View(_catalogRepository.GetAllCatalogItems());

        }
        */

        
        public async Task<IActionResult> Movie(int id)
        {
            return View(await _catalogRepository.GetCatalogItemByIdAsync(id));
        }

        public async Task<IActionResult> Index()
        {
            //return View(await _catalogRepository.GetAllCatalogItemsAsync());
            return View(await _catalogRepository.GetAllCatalogItemsAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
