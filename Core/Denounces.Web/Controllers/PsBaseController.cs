using Denounces.Domain.Entities;
using Denounces.Domain.Entities.Cor;
using Denounces.Infraestructure;
using Denounces.Infraestructure.Extensions;
using Denounces.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Denounces.Web.Controllers
{
    public class PsBaseController : Controller
    {
        protected ApplicationDbContext Context;
        protected IUserHelper UserHelper;
        protected GenericSelectList GenericSelectList;
        public readonly ICurrentUserFactory MyCurrentUser;


        public PsBaseController(ApplicationDbContext context,
            IUserHelper userHelper, ICurrentUserFactory currentUser)
        {
            Context = context;
            UserHelper = userHelper;
            GenericSelectList = new GenericSelectList();
            MyCurrentUser = currentUser;
        }

        //protected async Task<Owner> GetOwnerAsync()
        //{
        //    var user = await GetFullUserAsync();
        //    //var owner = await Context.Owners.FindAsync(user.Shop.Owner);
        //    return user.Shop.Owner;
        //}

        protected async Task<ApplicationUser> GetUserAsync()
        {
            return await UserHelper.GetUserByEmailAsync(User.Identity.Name);
        }

        protected async Task<ApplicationUser> GetFullUserAsync()
        {
            var user = await Context.Users//.Include(o => o.Owner)
                .Where(p => p.Email == User.Identity.Name).FirstOrDefaultAsync();

            return user;
        }



    }
}