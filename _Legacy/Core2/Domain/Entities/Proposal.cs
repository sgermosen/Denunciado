using Denounces.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public long Longitude { get; set; }

        public long Latitude { get; set; }

        public int PriorityNumber { get; set; }

        public DateTime? EndDate { get; set; }

        public int ProposalTypeId { get; set; }
        public ProposalType ProposalType { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public Proposal()
        {
            Votes = new Collection<Vote>();
        }


    }
}
