using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class NewIngredientViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
