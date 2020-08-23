using Denounces.Domain.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Domain.Entities
{
    public class ProposalType : BaseEntity
    {

          [Required]
        [StringLength(300, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 5)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        public ICollection<Proposal> Proposals { get; set; }

        public ProposalType()
        {
            Proposals = new Collection<Proposal>();
        }
    }
}
