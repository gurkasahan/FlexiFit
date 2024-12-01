// FlexiFit/Controllers/ClassesController.cs
using Microsoft.AspNetCore.Mvc;
using FlexiFit.Entities;
using FlexiFit.Services;
using System.Linq;
using FlexiFit.Entities.Models;

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

        // GET: Classes/List
        [HttpGet]
        public IActionResult List()
        {
            var classes = _classRepository.GetAll().ToList();
            return View(classes);
        }

        // GET: Classes/Details/{id}
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var cls = _classRepository.Get(id);
            if (cls == null)
            {
                return NotFound();
            }
            return View(cls);
        }

        // Additional actions like Create, Edit, Delete can be added if needed
    }
}
