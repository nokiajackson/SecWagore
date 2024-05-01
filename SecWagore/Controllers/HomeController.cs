using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecWagore.Models;
using SecWagore.Service;
using System.Diagnostics;

namespace SecWagore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       CampusService _campusService;


        public HomeController(ILogger<HomeController> logger, CampusService campusService)
        {
            _logger = logger;
            _campusService = campusService;
        }


        /// <summary>
        /// Get all campuses.
        /// </summary>
        /// <returns>A list of all campuses.</returns>
        [HttpGet("GetAllCampuses")]
        [ProducesResponseType(typeof(List<Campus>), 200)]
        public IActionResult GetAllCampuses()
        {
            var campuses = _campusService.GetAllCampus();
            return Ok(campuses);
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
