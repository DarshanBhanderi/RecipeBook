using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ingredients are required")]
        [StringLength(1000, ErrorMessage = "Ingredients cannot exceed 1000 characters")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Instructions are required")]
        [StringLength(2000, ErrorMessage = "Instructions cannot exceed 2000 characters")]
        public string Instructions { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created At")]
        [Required(ErrorMessage = "Creation date is required")]
        public DateTime CreatedAt { get; set; }
    }
}
