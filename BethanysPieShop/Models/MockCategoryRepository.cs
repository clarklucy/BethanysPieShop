using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories =>
            new List<Category>
            {
                new Category{CategoryId=1, CategoryName="Fruit Pies", Description="All-fruity"},
                new Category{CategoryId=2, CategoryName="Cheese Cakes", Description="Creamy, cheesy, cakes!"},
                new Category{CategoryId=3, CategoryName="Seasonal Pies", Description="Suit your mood with a seasonal pie"}
            };
    }
}
