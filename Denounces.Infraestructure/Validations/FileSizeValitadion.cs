using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Denounces.Infraestructure.Validations
{
    public class FileSizeValitadion: ValidationAttribute
    {
        private readonly int maxSizeInMegabytes;

        public FileSizeValitadion(int maxSizeInMegabytes)
        {
            this.maxSizeInMegabytes = maxSizeInMegabytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length > maxSizeInMegabytes * 1024 * 1024)
            {
                return new ValidationResult($"The size of the file is greather than {maxSizeInMegabytes} mb");
            }

            return ValidationResult.Success;
        }
    }
}
