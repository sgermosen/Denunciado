using Denounces.Domain.Entities;
using Denounces.Infraestructure;
using Denounces.Infraestructure.Extensions;
using Denounces.Repositories.Contracts;
using Denounces.Web.Helpers;
using Denounces.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Denounces.Web.Controllers
{
    public class ProposalsController : PsBaseController
    {
        private readonly IProposalRepository _proposalRepository;
        //private readonly IProposalTypeRepository _proposalTypeRepository;

        public ProposalsController(ApplicationDbContext context, IProposalRepository proposalRepository,
            //IProposalTypeRepository proposalTypeRepository,
            IUserHelper userHelper, ICurrentUserFactory currentUser) : base(context, userHelper, currentUser)
        {
            _proposalRepository = proposalRepository;
           // _proposalTypeRepository = proposalTypeRepository;
        }

        public IActionResult Index()
        {
            return View(_proposalRepository.GetAll());
        }

        public async Task<IActionResult> Create()
        {
            var types = await _proposalRepository.GetTypes();// _proposalTypeRepository.GetAll().ToList();

            var proposal = new ProposalCreateViewModel
            {
                Types = GenericSelectList.CreateSelectList(types, x => x.Id, x => x.Name)
            };

            return View(proposal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                proposal.CreatedUser = await GetUserAsync();
                await _proposalRepository.CreateAsync(proposal);
                return RedirectToAction(nameof(Index));
            }

            return View(proposal);

        }
    }
}