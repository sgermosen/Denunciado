using Denounces.Domain.Entities.Cor;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Web.Models
{
    public class ImageViewModel  
    {
        public int ImageIsFrom { get; set; } //1: Kids, 2: Activities, 3: Benefits, 4: Foods

        public IFormFile ImageFile { get; set; }

        public IEnumerable<Image> Images { get; set; }

        public long PersonId { get; set; }
       
        public long ActivityId { get; set; }
       
        public long BenefitId { get; set; }
       
        public long FoodId { get; set; }

        public long MyId { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string ImagenUrl { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long Id { get; set; }

        //TODO: change this for real url
        public string ImageFullPath => string.IsNullOrEmpty(ImagenUrl)
            ? "https://fundperdromartinez.azurewebsites.net/images/noimage.png"
            : $"https://fundperdromartinez.azurewebsites.net{ImagenUrl.Substring(1)}";
        //public string ImageFullPath => string.IsNullOrEmpty(ImagenUrl)
        //   ? "https://localhost:44357/images/noimage.png"
        //   : $"https://localhost:44357{ImagenUrl.Substring(1)}";

       
    }
}
