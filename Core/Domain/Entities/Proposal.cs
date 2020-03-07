using Denounces.Domain.Entities.Cor;
using Denounces.Domain.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Domain.Entities
{
    public class Proposal : AuditEntity, IBaseEntity
    {
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } //Resumen

        [Required]
        [MaxLength(5000)]
        public string Details { get; set; } //Details/ Legal Mark

        public int ProposalTypeId { get; set; }
        public ProposalType ProposalType { get; set; }

        public int Order { get; set; }

        //public DateTime CreationDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }


    }
}
