using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities.Models;
using FlexiFit.Services.Repositories;

namespace FlexiFit.Controllers
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Manages class-related operations.
    /// </summary>
    [Route("[controller]/[action]")]
    public class ClassesController : Controller
    {
        private readonly IRepository<Class> _classRepository;

        public ClassesController(IRepository<Class> classRepository)
        {
            _classRepository = classRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var classes = _classRepository.GetAll().ToList();
                return View(classes);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Optionally, return an error view
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var cls = _classRepository.GetById(id);
                if (cls == null)
                {
                    return NotFound();
                }
                return View(cls);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Classes/BookClasses/{memberId}
        [HttpGet("{memberId}")]
        public IActionResult BookClasses(int memberId)
        {
            try
            {
                var classes = _classRepository.GetAll().ToList();
                ViewBag.Classes = classes;
                ViewBag.MemberId = memberId;

                // Verify member exists
                var member = _memberRepository.GetById(memberId);
                if (member == null)
                {
                    return NotFound("Member not found.");
                }

        // Additional actions like Create, Edit, Delete can be added if needed
    }
}
