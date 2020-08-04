using Denounces.Domain.Entities.Cor;

namespace Denounces.Web.Areas.Administration.Controllers
{
    using CommonTasks.Data;
    using Denounces.Domain.Entities.Fun;
    using Denounces.Infraestructure;
    using Denounces.Infraestructure.Extensions;
    using Denounces.Web.Controllers;
    using Denounces.Web.Helpers;
    using Denounces.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Administration")]
    [Authorize(Roles = "Admin,User")]
    public class ZonesController : PsBaseController
    {
        public ZonesController(ApplicationDbContext context, IUserHelper userHelper, ICurrentUserFactory currentUser) : base(context, userHelper, currentUser)
        {

        }

        public async Task<IActionResult> DetailPlace(long id)
        {
            var owner = await GetOwnerAsync();
           // var vm = new ClassesViewModel { IsAdding = isAdding };

            var place = await Context.Places
                .FirstOrDefaultAsync(p => p.Id == id && p.CreatedUser.Shop.Owner == owner);
          
            return View(place);
        }

        public async Task<IActionResult> EditPlace(long id)
        {
            var vm = new Place();
            var model = await Context.Places.FindAsync(id);
            model?.Transfer(ref vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlace(Place vm)
        {
            var model = await Context.Places.FindAsync(vm.Id);
            model.Name = vm.Name;
          
            Context.Update(model);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeletePlace(long Id)
        {
            var vm = new Place();
            var model = await Context.Places.FindAsync(Id);
            model?.Transfer(ref vm);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlace(Place vm)
        {
            var model = await Context.Places.FindAsync(vm.Id);
            model.Deleted = true;

            Context.Update(model);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var owner = await GetOwnerAsync();
            var zones = await Context.Zones//.Include(p => p.Places)//.ThenInclude(p => p.Occupants)
                .Where(p => p.CreatedUser.Shop.Owner == owner).ToListAsync();

            return View(zones);
        }

        public async Task<IActionResult> Details(long id)
        {
            var owner = await GetOwnerAsync();
            var det = await Context.Zones
                .Include(p => p.Places).ThenInclude(p => p.Occupants)
                .Include(p=>p.Places).ThenInclude(p => p.Status)
                .Include(p => p.Places).ThenInclude(p => p.Occupants).ThenInclude(p => p.Classe)
                .Include(p => p.Places).ThenInclude(p => p.Occupants).ThenInclude(p => p.Person)
                //.Include(p => p.Members).ThenInclude(p => p.Member).ThenInclude(p => p.Status)
                //.Include(p => p.Members).ThenInclude(p => p.Member).ThenInclude(p => p.MemberType)
                .FirstOrDefaultAsync(p => p.Id == id && p.CreatedUser.Shop.Owner == owner);

            return View(det);
        }

        public IActionResult AddPlace(long id)
        {
            var cal = new Place
            {
                ZoneId = id,
            };

            return View(cal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlace(Place vm)
        {
            var owner = await GetOwnerAsync();

            if (ModelState.IsValid)
            {
                var user = await GetUserAsync();
                vm.Id = 0;
                vm.CreatedUser = user;
                vm.StatusId = 1;

                Context.Add(vm);

                await Context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}", new { id = vm.ZoneId });
            }

            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Zone vm)
        {
            var owner = await GetOwnerAsync();

            if (ModelState.IsValid)
            {
                var user = await GetUserAsync();
                vm.CreatedUser = user;

                Context.Add(vm);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var vm = new Zone();
            var model = await Context.Zones.FindAsync(id);
            model?.Transfer(ref vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Zone vm)
        {
            var model = await Context.Zones.FindAsync(vm.Id);
            model.Name = vm.Name;

            Context.Update(model);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long Id)
        {
            var vm = new Zone { };
            var model = await Context.Zones.FindAsync(Id);
            model?.Transfer(ref vm);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Zone vm)
        {
            var model = await Context.Zones.FindAsync(vm.Id);
            model.Deleted = true;

            Context.Update(model);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}