namespace Denounces.Web.Areas.Administration.Controllers
{
    using CommonTasks.Data;
    using Denounces.Domain.Entities.Cor;
    using Denounces.Domain.Entities.Med;
    using Denounces.Domain.Entities.Sch;
    using Denounces.Domain.Helpers;
    using Denounces.Infraestructure;
    using Denounces.Infraestructure.Extensions;
    using Denounces.Web.Controllers;
    using Denounces.Web.Helpers;
    using Denounces.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Administration")]
    [Authorize(Roles = "Admin,User")]
    public class ReportsController : PsBaseController
    {
         public ReportsController(ApplicationDbContext context, IUserHelper userHelper, ICurrentUserFactory currentUser) : base(context, userHelper, currentUser)
        { 
        }

        //public async Task<IActionResult> IndexFood(string dateFrom, string dateTo, long foodId = 0)
        //{
        //    var type = await Context.MemberTypes
        //               .FirstOrDefaultAsync(p => p.CreatedUser.Shop.Owner.Id == MyCurrentUser.Get.OwnerId);

        //    var owner = await GetOwnerAsync();
        //    var kids = Context.People.Include(p => p.MemberType)
        //        .Where(p => p.CreatedUser.Shop.Owner == owner && p.MemberType != type);

        //    if (filterType > 0)
        //    {
        //        kids = kids.Where(p => p.MemberTypeId == filterType);
        //    }
        //    ViewData["MemberTypes"] = Context.MemberTypes.
        //        Where(p => p.CreatedUser.Shop.Owner == owner)
        //               .Select(n => new SelectListItem
        //               {
        //                   Value = n.Id.ToString(),
        //                   Text = n.Name
        //               }).ToList();

        //    var memberTypes = await Context.MemberTypes.Include(p => p.People)
        //        .Where(p => p.CreatedUser.Shop.Owner == owner)
        //        .ToListAsync();

        //    var memberTypeList = new Collection<MemberTypeViewModel>();
        //    foreach (var item in memberTypes)
        //    {
        //        memberTypeList.Add(new MemberTypeViewModel
        //        {
        //            Id = item.Id,
        //            Name = item.Name,
        //            Quantity = item.People.Count()
        //        });
        //    }

        //    ViewBag.MemberTypeList = memberTypeList;

        //    return View(await kids.ToListAsync());
        //}

        //public async Task<IActionResult> Details(long id)
        //{
        //    var owner = await GetOwnerAsync();
        //    var vm = new KidViewModel();

        //    var countries = await Context.Countries.OrderBy(p => p.Name).ToListAsync();
        //    var bloodTypes = await Context.BloodTypes.OrderBy(p => p.Name).ToListAsync();
        //    var insurances = await Context.Insurances.OrderBy(p => p.Name).ToListAsync();
        //    var tandas = await Context.Tandas.OrderBy(p => p.Name).ToListAsync();
        //    var religions = await Context.Religions.OrderBy(p => p.Name).ToListAsync();
        //    var courses = await Context.Courses.Where(p => p.CreatedUser.Shop.Owner == owner)
        //        .OrderBy(p => p.Name).ToListAsync();
        //    var schools = await Context.Schools.Where(p => p.CreatedUser.Shop.Owner == owner)
        //      .OrderBy(p => p.Name).ToListAsync();
        //    var status = await Context.Status.OrderBy(p => p.Name).ToListAsync();
        //    var memberTypes = await Context.MemberTypes.Where(p => p.CreatedUser.Shop.Owner == owner)
        //     .OrderBy(p => p.Name).ToListAsync();

        //    ViewData["Day"] = Enumerable.Range(1, 31)
        //        .Select(n => new SelectListItem
        //        {
        //            Value = n.ToString(),
        //            Text = n.ToString()
        //        }).ToList();

        //    ViewData["Month"] = Enumerable.Range(1, 12)
        //       .Select(n => new SelectListItem
        //       {
        //           Value = n.ToString(),
        //           Text = n.ToString()
        //       }).ToList();

        //    ViewData["Year"] = Enumerable.Range(1919, 2019)
        //       .Select(n => new SelectListItem
        //       {
        //           Value = n.ToString(),
        //           Text = n.ToString()
        //       }).ToList();

        //    vm.Religions = GenericSelectList.CreateSelectList(religions, x => x.Id, x => x.Name);
        //    vm.Countries = GenericSelectList.CreateSelectList(countries, x => x.Id, x => x.Denomym);
        //    vm.BloodTypes = GenericSelectList.CreateSelectList(bloodTypes, x => x.Id, x => x.Name);
        //    vm.Insurances = GenericSelectList.CreateSelectList(insurances, x => x.Id, x => x.Name);
        //    vm.Tandas = GenericSelectList.CreateSelectList(tandas, x => x.Id, x => x.Name);
        //    vm.Courses = GenericSelectList.CreateSelectList(courses, x => x.Id, x => x.Name);
        //    vm.Schools = GenericSelectList.CreateSelectList(schools, x => x.Id, x => x.Name);
        //    vm.StatusSelList = GenericSelectList.CreateSelectList(status, x => x.Id, x => x.Name);
        //    vm.MemberTypes = GenericSelectList.CreateSelectList(memberTypes, x => x.Id, x => x.Name);

        //    var person = await Context.People
        //        .Include(p => p.Califications).ThenInclude(x => x.Subject)
        //        .Include(p => p.Members).ThenInclude(x => x.Signature)
        //        .Include(p => p.Beneficiaries).ThenInclude(x => x.Benefit)
        //        .Include(p => p.Comensals).ThenInclude(x => x.Food)
        //        .Include(p => p.Participants).ThenInclude(x => x.Activity)
        //        .Include(p => p.EconomicPerfils)
        //        .Where(p => p.CreatedUser.Shop.Owner == owner && p.Id == id)
        //        .FirstOrDefaultAsync();

        //    var patient = await Context.Patients
        //        .Where(p => p.CreatedUser.Shop.Owner == owner && p.Person == person)
        //        .FirstOrDefaultAsync();

        //    var kid = await Context.Patients
        //       .Where(p => p.CreatedUser.Shop.Owner == owner && p.Person == person)
        //       .FirstOrDefaultAsync();

        //    kid.Transfer(ref vm);
        //    patient.Transfer(ref vm);
        //    person.Transfer(ref vm);

        //    //vm.Id = person.Id;

        //    var relPersons = await Context.RelatedPeople
        //        .Where(p => p.RelatedPerson == person)
        //        .Include(p => p.RelationType)
        //        .Include(p => p.RelationedPerson)
        //        .ThenInclude(x => x.Patients)
        //                        .ToListAsync();

        //    vm.RelationedPersons = relPersons;

        //    return View(vm);
        //}

        //public async Task<IActionResult> Create()
        //{
        //    var owner = await GetOwnerAsync();
        //    var countries = await Context.Countries.OrderBy(p => p.Name).ToListAsync();
        //    var bloodTypes = await Context.BloodTypes.OrderBy(p => p.Name).ToListAsync();
        //    var insurances = await Context.Insurances.OrderBy(p => p.Name).ToListAsync();
        //    var tandas = await Context.Tandas.OrderBy(p => p.Name).ToListAsync();
        //    var religions = await Context.Religions.OrderBy(p => p.Name).ToListAsync();
        //    var courses = await Context.Courses.Where(p => p.CreatedUser.Shop.Owner == owner).OrderBy(p => p.Name).ToListAsync();
        //    var schools = await Context.Schools.Where(p => p.CreatedUser.Shop.Owner == owner).OrderBy(p => p.Name).ToListAsync();
        //    var status = await Context.Status.Where(p => p.Table == "ALL").OrderBy(p => p.Name).ToListAsync();
        //    var memberTypes = await Context.MemberTypes.Where(p => p.CreatedUser.Shop.Owner == owner)
        //     .OrderBy(p => p.Name).ToListAsync();

        //    ViewData["Day"] = Enumerable.Range(1, 31)
        //        .Select(n => new SelectListItem
        //        {
        //            Value = n.ToString(),
        //            Text = n.ToString()
        //        }).ToList();

        //    ViewData["Month"] = Enumerable.Range(1, 12)
        //       .Select(n => new SelectListItem
        //       {
        //           Value = n.ToString(),
        //           Text = n.ToString()
        //       }).ToList();

        //    ViewData["Year"] = Enumerable.Range(1919, 2019)
        //       .Select(n => new SelectListItem
        //       {
        //           Value = n.ToString(),
        //           Text = n.ToString()
        //       }).ToList();

        //    var kid = new KidViewModel
        //    {
        //        Religions = GenericSelectList.CreateSelectList(religions, x => x.Id, x => x.Name),
        //        Countries = GenericSelectList.CreateSelectList(countries, x => x.Id, x => x.Denomym),
        //        BloodTypes = GenericSelectList.CreateSelectList(bloodTypes, x => x.Id, x => x.Name),
        //        Insurances = GenericSelectList.CreateSelectList(insurances, x => x.Id, x => x.Name),
        //        Tandas = GenericSelectList.CreateSelectList(tandas, x => x.Id, x => x.Name),
        //        Courses = GenericSelectList.CreateSelectList(courses, x => x.Id, x => x.Name),
        //        Schools = GenericSelectList.CreateSelectList(schools, x => x.Id, x => x.Name),
        //        StatusSelList = GenericSelectList.CreateSelectList(status, x => x.Id, x => x.Name),
        //        MemberTypes = GenericSelectList.CreateSelectList(memberTypes, x => x.Id, x => x.Name),
        //    };

        //    return View(kid);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(KidViewModel vm)
        //{
        //    var owner = await GetOwnerAsync();

        //    if (string.IsNullOrEmpty(vm.InsuranceNumber) == false)
        //    {
        //        var pat = Context.Patients.Any(p => p.InsuranceNumber.ToUpper() == vm.InsuranceNumber.ToUpper()
        //                                            && p.Person.CreatedUser.Shop.Owner == owner);

        //        if (pat)
        //        {
        //            ModelState.AddModelError(string.Empty, "Este Numero de Seguro ya esta registrado");
        //        }
        //    }

        //    if (string.IsNullOrEmpty(vm.Name) == false && string.IsNullOrEmpty(vm.LastName) == false)
        //    {
        //        var pat = Context.People.Any(p => p.Name.ToUpper() == (vm.Name.ToUpper())
        //                                          && p.LastName.ToUpper() == (vm.LastName.ToUpper())
        //                                            && p.CreatedUser.Shop.Owner == owner);

        //        if (pat)
        //        {
        //            ModelState.AddModelError(string.Empty, "Este Paciente ya esta registrado");
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var path = string.Empty;

        //        if (vm.ImageFile != null && vm.ImageFile.Length > 0)
        //        {
        //            var guid = Guid.NewGuid().ToString();
        //            var file = $"{guid}.jpg";

        //            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Kids", file);

        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                await vm.ImageFile.CopyToAsync(stream);
        //            }

        //            path = $"~/images/Kids/{file}";
        //        }

        //        vm.Imagen = path;
        //        vm.Record = recordHelper.GenerateRecord(owner);

        //        if (string.IsNullOrEmpty(vm.Record2))
        //        {
        //            vm.Record2 = $"{owner.PrefixExp}{vm.Record:00000}";
        //        }

        //        var user = await GetUserAsync();

        //        DateTime bornDate;
        //        try
        //        {
        //            bornDate = DateTime.Parse($"{vm.Year.ToString()}/{string.Format(vm.Month.ToString(), "00")}/{string.Format(vm.Day.ToString(), "00")}");
        //        }
        //        catch (Exception)
        //        {
        //            bornDate = DateTime.UtcNow;
        //        }

        //        var person = new Person
        //        {
        //            Rnc = vm.Rnc,
        //            Name = vm.Name,
        //            LastName = vm.LastName,
        //            BornDate = bornDate,
        //            Gender = vm.Gender,
        //            SchoolLevelId = 1,
        //            CountryId = vm.CountryId,
        //            Cel = vm.Cel,
        //            Tel = vm.Tel,
        //            Tel2 = vm.Tel2,
        //            Tel3 = vm.Tel3,
        //            Tel4 = vm.Tel4,
        //            Email = vm.Email,
        //            MaritalSituation = MaritalSituation._NA,
        //            OcupationId = 1,
        //            ReligionId = vm.ReligionId,
        //            Address = vm.Address,
        //            Imagen = vm.Imagen,
        //            Record2 = vm.Record2,
        //            Record = vm.Record,
        //            CreditAmount = 0,
        //            DebAmount = 0,
        //            WastedAmount = 0,
        //            Salary = 0,
        //            MemberTypeId = vm.MemberTypeId,
        //            BloodTypeId = vm.BloodTypeId,
        //            Age = vm.Age,
        //            CreatedUser = user,
        //            //Nss =""
        //        };

        //        var patient = new Patient
        //        {
        //            InsuranceId = vm.InsuranceId,
        //            InsuranceNumber = vm.InsuranceNumber,
        //            Person = person,
        //            Alergies = vm.Alergies,
        //            Illness = vm.Illness,
        //            Medicaments = vm.Medicaments,
        //            MedicamentationTime = vm.MedicamentationTime,
        //        };

        //        var school = new School();
        //        if (!string.IsNullOrEmpty(vm.SchoolStr))
        //        {
        //            var schoolfi = Context.Schools.Where(p => p.Name == vm.SchoolStr).FirstOrDefault();
        //            if (schoolfi == null)
        //            {
        //                school.Name = vm.SchoolStr;
        //                Context.Add(school);
        //            }
        //        }
        //        if (vm.SchoolId > 0)
        //        {
        //            var schoolfi = Context.Schools.Find(vm.SchoolId);
        //            if (schoolfi != null)
        //            {
        //                vm.SchoolStr = schoolfi.Name;
        //            }
        //        }

        //        var kid = new Kid
        //        {
        //            Person = person,
        //            PediatricName = vm.PediatricName,
        //            MedicalPlace = vm.MedicalPlace,
        //            NoBrothers = vm.NoBrothers,
        //            Place = vm.Place,
        //            WhoLive = vm.WhoLive,
        //            WhoSleep = vm.WhoSleep,
        //            SchoolStr = vm.SchoolStr,
        //            TandaId = vm.TandaId,
        //            CourseId = vm.CourseId,
        //            TelSchool = vm.TelSchool,
        //            AddressSchool = vm.AddressSchool,
        //            Fired = vm.Fired,
        //            HasLostSchoolarYear = vm.HasLostSchoolarYear,
        //            DificultSubject = vm.DificultSubject,
        //            BornPlace = vm.BornPlace
        //        };

        //        Context.Add(patient);
        //        Context.Add(kid);
        //        await Context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var countries = await Context.Countries.OrderBy(p => p.Name).ToListAsync();
        //    var bloodTypes = await Context.BloodTypes.OrderBy(p => p.Name).ToListAsync();
        //    var insurances = await Context.Insurances.OrderBy(p => p.Name).ToListAsync();
        //    var tandas = await Context.Tandas.OrderBy(p => p.Name).ToListAsync();
        //    var religions = await Context.Religions.OrderBy(p => p.Name).ToListAsync();
        //    var courses = await Context.Courses.Where(p => p.CreatedUser.Shop.Owner == owner).OrderBy(p => p.Name).ToListAsync();
        //    var schools = await Context.Schools.Where(p => p.CreatedUser.Shop.Owner == owner).OrderBy(p => p.Name).ToListAsync();
        //    var status = await Context.Status.OrderBy(p => p.Name).ToListAsync();
        //    var memberTypes = await Context.MemberTypes.Where(p => p.CreatedUser.Shop.Owner == owner)
        //        .OrderBy(p => p.Name).ToListAsync();

        //    ViewData["Day"] = Enumerable.Range(1, 31)
        //        .Select(n => new SelectListItem
        //        {
        //            Value = n.ToString(),
        //            Text = n.ToString()
        //        }).ToList();

        //    ViewData["Month"] = Enumerable.Range(1, 12)
        //       .Select(n => new SelectListItem
        //       {
        //           Value = n.ToString(),
        //           Text = n.ToString()
        //       }).ToList();

        //    ViewData["Year"] = Enumerable.Range(1919, 2019)
        //       .Select(n => new SelectListItem
        //       {
        //           Value = n.ToString(),
        //           Text = n.ToString()
        //       }).ToList();


        //    vm.Religions = GenericSelectList.CreateSelectList(religions, x => x.Id, x => x.Name);
        //    vm.Countries = GenericSelectList.CreateSelectList(countries, x => x.Id, x => x.Denomym);
        //    vm.BloodTypes = GenericSelectList.CreateSelectList(bloodTypes, x => x.Id, x => x.Name);
        //    vm.Insurances = GenericSelectList.CreateSelectList(insurances, x => x.Id, x => x.Name);
        //    vm.Tandas = GenericSelectList.CreateSelectList(tandas, x => x.Id, x => x.Name);
        //    vm.Courses = GenericSelectList.CreateSelectList(courses, x => x.Id, x => x.Name);
        //    vm.Schools = GenericSelectList.CreateSelectList(schools, x => x.Id, x => x.Name);
        //    vm.StatusSelList = GenericSelectList.CreateSelectList(status, x => x.Id, x => x.Name);
        //    vm.MemberTypes = GenericSelectList.CreateSelectList(memberTypes, x => x.Id, x => x.Name);

        //    return View(vm);
        //}

    }
}