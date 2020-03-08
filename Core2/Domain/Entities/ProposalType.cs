using Denounces.Domain.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Domain.Entities
{
    public class ProposalType : AuditEntity,  IBaseEntity
    {

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        public bool IsActive { get; set; }

        public int Order { get; set; }

        public ICollection<Proposal> Proposals { get; set; }

        public ProposalType()
        {
            Proposals = new Collection<Proposal>();
        }
    }
}
