using Denounces.Infraestructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Denounces.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly ApplicationDbContext _context;

        public CombosHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboProposalTypes()
        {
            List<SelectListItem> list = _context.ProposalTypes.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a proposal type...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboProposalTypes(long id)
        {
            List<SelectListItem> list = _context.ProposalTypes
                               .Where(gd => gd.Id == id)
                .Select(gd => new SelectListItem
                {
                    Text = gd.Name,
                    Value = $"{gd.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a proposal type...]",
                Value = "0"
            });

            return list;
        }

    }
}
