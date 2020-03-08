using Denounces.Infraestructure;
using Denounces.Repositories.Contracts;

namespace Denounces.Repositories.Implementations.Cor
{
    using Domain.Entities;

    public class ProposalTypeRepository : Repository<ProposalType>, IProposalTypeRepository
    {
        public ProposalTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
