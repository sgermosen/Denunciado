using Denounces.Domain.Entities;
using Denounces.Domain.Entities.Cor;
using Denounces.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Web.Areas.Administration.Models
{
    public class EmployeeView : ApplicationUser
    {
        
        [Display(Name = "Fecha de Ingreso")]
        ////[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Display(Name = "Género")]
        public Gender Gender { get; set; }

        [Display(Name = "Nivel Escolar")]
        public SchoolLevel SchoolLevel { get; set; }

        [Display(Name = "Nacionalidad")]
        public Country Country { get; set; }

        [MaxLength(15, ErrorMessage = "La longitud maxima del campo es {1} caracteres")]
        [DataType(DataType.PhoneNumber)]
        //    [Index("Person_Cel_Index", IsUnique = true)]
        [Display(Name = "Celular")]
        public string Cel { get; set; }

        [Display(Name = "Estatus Marital")]
        public MaritalSituation MaritalSituation { get; set; }

        [Display(Name = "Ocupacion")]
        public Ocupation Ocupation { get; set; }

        [Display(Name = "Religión")]
        public Religion Religion { get; set; }
          
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string Imagen { get; set; }

        [Display(Name = "Codigo")]
        public string Record2 { get; set; }

        public int Record { get; set; }

        public long CountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        public long OcupationId { get; set; }
        public IEnumerable<SelectListItem> Ocupations { get; set; }

        public long ReligionId { get; set; }
        public IEnumerable<SelectListItem> Religions { get; set; }

        public long SchoolLevelId { get; set; }
        public IEnumerable<SelectListItem> SchoolLevels { get; set; }

        public IFormFile ImageFile { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        //public ApplicationUser User { get; set; }
    }
}
