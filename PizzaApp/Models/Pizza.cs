using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public List<Ingredient> Ingredients { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}
