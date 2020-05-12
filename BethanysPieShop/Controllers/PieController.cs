using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
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


        //---- original List action
        //public ViewResult List()
        //{
        //    PiesListViewModel piesListViewModel = new PiesListViewModel();
        //    piesListViewModel.Pies = _pieRepository.AllPies;
        //    piesListViewModel.CurrentCategory = "Cheese Cakes";

        //    return View(piesListViewModel); //this is now the ViewData.Model property of the view result being returned
        //}

        public IActionResult Details (int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }


        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PiesListViewModel
            { 
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }
    }
}
