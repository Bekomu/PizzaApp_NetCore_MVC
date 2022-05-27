using Microsoft.AspNetCore.Http;
using PizzaApp.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class EditPizzaViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<int> SelectedIngredients { get; set; } = new List<int>();

        public List<Ingredient> Ingredients { get; set; }

        [Required]
        [ValidPhoto(maximumPhotoSize = 2048)]
        public IFormFile Photo { get; set; }
    }
}
