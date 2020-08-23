using Denounces.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Domain.Entities
{
    public class ProposalFile : AuditEntity, IBaseEntity
    {
        public object MyProperty { get; set; }
        public string Description { get; set; } //Resumen
         
        public int ProposalId { get; set; }
        public Proposal Proposal { get; set; }
           }
}
