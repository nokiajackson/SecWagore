using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecWagore.Enum;
using SecWagore.Heplers;
using SecWagore.Models;
using SecWagore.Service;
using System.Diagnostics;

namespace SecWagore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       CampusService _campusService;


        public HomeController(ILogger<HomeController> logger, CampusService campusService)
        {
            _logger = logger;
            _campusService = campusService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {

            return View();
        }


        [AllowAnonymous]
        public IActionResult EntryRecord()
        {
            List<KeyName> purposeOptions = EnumeratorHelper.GetEnumDescriptions<Purpose>();
            ViewBag.PurposeOptions = purposeOptions.ToArray();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
