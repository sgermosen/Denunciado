using Denounces.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denounces.Domain.Entities
{
    public class ProposalType : AuditEntity
    {

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }
        
        public bool IsActive { get; set; }
              
        public int  Order { get; set; }

        public ICollection<Proposal> Proposals { get; set; }
    }
}
