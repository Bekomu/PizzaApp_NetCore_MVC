using Microsoft.AspNetCore.Http;
using PizzaApp.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class CreatePizzaViewModel
    {
        [Required]
        public string Name { get; set; }

        public int[] SelectedIngredients { get; set; } = Array.Empty<int>();

        public List<Ingredient> Ingredients { get; set; }

        [Required]
        [ValidPhoto(maximumPhotoSize = 2048)]
        public IFormFile Photo { get; set; }
    }
}
