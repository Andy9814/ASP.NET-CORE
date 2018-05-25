using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace ASPNETExercises.Models
{
    public class CategoryViewModel
    {
        private List<Category> _categories;
        public IEnumerable<MenuItem> MenuItems { get; set; }
        [Required]
        public int Qty { get; set; }
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> GetCategories()
        {
            return _categories.Select(category => new SelectListItem
            {
                Text = category.Name,
                Value = Convert.ToString(category.Id)
            });
        }
        public void SetCategories(List<Category> cats)
        {
            _categories = cats;
        }
    }
}
