using Denounces.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Domain.Entities
{
    //this is the individual complaint of each user
    public class Proposal : AuditEntity, IBaseEntity
    {

        // [StringLength(250, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 10)]
        public string Address { get; set; } //where its happening this

        public long ProvinceId { get; set; }
        public Province Province { get; set; }

        //[Required]
        // [MaxLength(2000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } //Resumen

        //  [Required]
        //  [MaxLength(5000)]
        public string Details { get; set; } //Details/ Legal Mark not requied on complaint

        public DateTime? EndDate { get; set; }

        public long Longitude { get; set; }

        public long Latitude { get; set; }

        public int PriorityNumber { get; set; }

        public long ProposalTypeId { get; set; }
        public ProposalType ProposalType { get; set; }

        public long StatusId { get; set; }
        public Status Status { get; set; }

        public ICollection<Vote> Votes { get; set; }
        public ICollection<ProposalFile> ProposalFiles { get; set; }
    }
}
