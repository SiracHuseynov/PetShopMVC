using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Business.Exceptions;
using PetShop.Business.Services.Abstracts;
using PetShop.Core.Models;

namespace PetShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]

    public class ProfessionalController : Controller
    {
        private readonly IProfessionalService _professionalService;

        public ProfessionalController(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }

        public IActionResult Index()
        {
            var professionals = _professionalService.GetAllProfessionals();
            return View(professionals);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Professional professional)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _professionalService.AddAsyncProfessional(professional);
            }

            catch (ImageNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();

            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existProfessional = _professionalService.GetProfessional(x=> x.Id == id);
            if (existProfessional == null)
                return NotFound();

            return View(existProfessional);
        }


        [HttpPost]
        public IActionResult Update(Professional professional)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _professionalService.UpdateProfessional(professional.Id, professional);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();

            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existProfessional = _professionalService.GetProfessional(x => x.Id == id);
            if (existProfessional == null)
                return NotFound();

            return View(existProfessional);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _professionalService.DeleteProfessional(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
