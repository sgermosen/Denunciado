using Denounces.Infraestructure;
using Denounces.Repositories.Contracts;
using System.Threading.Tasks;

namespace Denounces.Repositories.Implementations.Cor
{
    using Domain.Entities;

    public class ProposalRepository : Repository<Proposal>, IProposalRepository
    {
        public ProposalRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Proposal> AddProposalAsync(Proposal entity)
        {
            entity.PriorityNumber = 9;
            entity.StatusId = 1;
            entity.ProposalTypeId = 1;

            await Context.Proposals.AddAsync(entity);
            await SaveAllAsync();
            return entity;
        }
    }
}
