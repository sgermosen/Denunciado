namespace Denounces.Domain.Entities.Cor
{
    using Helpers;
    using System.ComponentModel.DataAnnotations;

    public class Image : AuditEntity, IBaseEntity
    {
        public int  ImageIsFrom { get; set; } //1: Kids, 2: Activities, 3: Benefits, 4: Foods, 

       public int? PersonId { get; set; }
        public Person Person { get; set; }

        public int? ComplaintId { get; set; }
        public Complaint Complaint { get; set; }
         
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string ImagenUrl { get; set; }

        public string Description { get; set; }

        //TODO: Real Image url 
        public string ImageFullPath => string.IsNullOrEmpty(ImagenUrl)
            ? "https://localhost:44357/images/noimage.png"
            : $"https://localhost:44357{ImagenUrl.Substring(1)}";



    }
}
