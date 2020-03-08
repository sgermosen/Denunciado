namespace Denounces.Domain.Entities
{
    using Helpers;
    using System.ComponentModel.DataAnnotations;

    public class Image : AuditEntity, IBaseEntity
    {       
       public int? UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int? ProposalId { get; set; }
        public Proposal Proposal { get; set; }
         
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
