namespace Denounces.Domain.Entities.Cor
{
    using Helpers;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Person : AuditEntity, IBaseEntity
    {
        public Gender Gender { get; set; }

        public int SchoolLevelId { get; set; }
        public SchoolLevel SchoolLevel { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(13)]
        public string Cel { get; set; }

        public MaritalSituation MaritalSituation { get; set; }

        public int OcupationId { get; set; }
        public Ocupation Ocupation { get; set; }

        public int ReligionId { get; set; }
        public Religion Religion { get; set; }

        [MaxLength(150)]
        public string PlaceOfWork { get; set; }

        [MaxLength(30)]
        public string Age { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Salary { get; set; }

        [MaxLength(15)]
        public string Nss { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public int ProvinceId { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Picture { get; set; }

        public string NickName { get; set; }

        public int FavoriteIdeologyId { get; set; }

        //  public int  Points { get; set; }

        //TODO: CHange for real url

        public string ImageFullPath => string.IsNullOrEmpty(Picture)
            ? "  https://localhost:44357/images/noimage.png"
            : $"  https://localhost:44357{Picture.Substring(1)}";

        public virtual ICollection<Image> Images { get; set; }

        public Person()
        {
            Images = new Collection<Image>();
        }

    }
}
