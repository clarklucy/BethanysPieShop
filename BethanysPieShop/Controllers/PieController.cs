using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        
        //these are fields that don't get initialised automatically 
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        //use constructor to initialise
        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
          /*passing IPieRepository and ICategoryRepository (model) classes to the constructor method.
            These are injected into our controller as they are registered in the services - don't need to "new them up" */
        {
            //local = one going to be injected
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }


        public ViewResult List()
        {
            return View(_pieRepository.AllPies);
        }
    }
}
