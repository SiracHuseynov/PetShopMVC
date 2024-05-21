using Microsoft.AspNetCore.Mvc;
using PetShop.Business.Services.Abstracts;
using System.Diagnostics;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProfessionalService _professionalService;

        public HomeController(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }

        public IActionResult Index()
        {
            var professions = _professionalService.GetAllProfessionals();
            return View(professions);
        }

      
    }
}
