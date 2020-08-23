using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Denounces.Domain.Helpers;
using Microsoft.AspNetCore.Http;

namespace Denounces.Infraestructure.Validations
{
    public class FileTypeValidation: ValidationAttribute
    {
        private readonly string[] validTypes;

        public FileTypeValidation(string[] validTypes)
        {
            this.validTypes = validTypes;
        }

        public FileTypeValidation(FileType groupTypeFile)
        {
            if (groupTypeFile == FileType.Image)
            {
                validTypes = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
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

            if (!validTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult($"The file type must be one of this: {string.Join(", ", validTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}
