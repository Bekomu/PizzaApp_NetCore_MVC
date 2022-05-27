using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Attributes
{
    public class ValidPhotoAttribute : ValidationAttribute
    {
        public int maximumPhotoSize { get; set; } = 1024;


        public override bool IsValid(object value)
        {
            IFormFile formFile = value as IFormFile; // geçersiz gelirse null atsın diye böyle cast

            if (formFile == null)
                return true;

            if (!formFile.ContentType.StartsWith("image/"))
            {
                ErrorMessage = "Invalid photo file";
                return false;
            }
            else if (formFile.Length > maximumPhotoSize * 1024)
            {
                ErrorMessage = $"File size is too big. Maximum size : {maximumPhotoSize}kb.";
                return false;
            }
            else
                return true;
        }
    }
}
