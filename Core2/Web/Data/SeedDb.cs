using System.Linq;

namespace Denounces.Web.Data
{
    using Domain.Entities;
    using Domain.Entities.Cor;
    using Domain.Entities.Med;
    using Helpers;
    using Infraestructure;
    using Denounces.Domain.Entities.Cpo;
    using Denounces.Domain.Entities.Fun;
    using Denounces.Domain.Entities.Pos;
    using Denounces.Domain.Entities.Sch;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class SeedDb
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(ApplicationDbContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            // await _context.Database.EnsureCreatedAsync();

            //if (!_context.Owners.Any())
            //{
            //    await CheckRolesAsync();
            //    var owner = new Owner
            //    {
            //        Code = "Mersy",
            //        Name = "Mersy",
            //        LastName = "RD",
            //        Header1 = "Hospital General Regional",
            //        Header2 = "Doctor Mersy RD",
            //        Header3 = "RNC 5-30-58349-8",
            //        Footer1 = "Carretera Nuevo, No. 28, Los Merca",
            //        Footer2 = "Tels: (849) 207-7714, (829) 349-5083",
            //        Footer3 = "Web: MersyRd.com, Email: Info@MersyRd.com",
            //        Imagen = "~/images/logoPraySoft_blue.png",
            //        Email = "mersyrd@gmail.com"
            //    };
            //    var shop = new Shop { Name = "Sucursal Mersy", Owner = owner };

            //    await _context.SaveChangesAsync();
            //    await CheckUser("elis@gmail.com", "Elis", "Pascual", "User", shop);
            //    await CheckUser("starling@gmail.com", "Starling", "Germosen", "User", shop);
            //    await CheckUser("mersyrd@gmail.com", "Mersy", "RD", "Admin", shop);
            //    await CheckUser("sgrysoft@gmail.com", "Starling", "Germosen", "Admin", shop);

            
            //    await CheckUser("admin@fundacionpedromartinez.com", "Admin", "Fundacion", "Admin", shop);
            //    await CheckUser("user@fundacionpedromartinez.com", "User", "Fundacion", "User", shop);
            //    await CheckUser("supervisor@fundacionpedromartinez.com", "User", "Fundacion", "Supervisor", shop);
            //    await CheckUser("superuser@fundacionpedromartinez.com", "User", "Fundacion", "SuperUser", shop);

            //    await CheckUser("AlexandraJoga@fundacionpedromartinez.com", "Alexandra", "Joga", "Supervisor", shop);
            //    await CheckUser("DomingaSantana@fundacionpedromartinez.com", "Dominga", "Santana", "Admin", shop);
            //    await CheckUser("IleneArias@fundacionpedromartinez.com", "Ilene", "Arias", "Admin", shop);
            //    await CheckUser("JhonanMeran@fundacionpedromartinez.com", "Jhonan", "Meran", "Admin", shop);
            //    await CheckUser("JuanaConterras@fundacionpedromartinez.com", "Juana", "Conterras", "Supervisor", shop);
            //    await CheckUser("NeulisVizcaino@fundacionpedromartinez.com", "Neulis", "Vizcaino", "Admin", shop);
            //    await CheckUser("YerannyMercedes@fundacionpedromartinez.com", "Yeranny", "Mercedes", "Supervisor", shop);
            //    await CheckUser("CarolinaCruz@fundacionpedromartinez.com", "Carolina", "Cruz", "SuperUser", shop);
            //    await CheckUser("CharlesChesser@fundacionpedromartinez.com", "Charles", "Chesser", "SuperUser", shop);
            //    await CheckUser("DahianaBurgos@fundacionpedromartinez.com", "Dahiana", "Burgos", "SuperUser", shop);
            //    await CheckUser("DaselisMota@fundacionpedromartinez.com", "Daselis", "Mota", "SuperUser", shop);
            //    await CheckUser("JosePolanco@fundacionpedromartinez.com", "José", "Polanco", "SuperUser", shop);
            //    await CheckUser("MasielGomez@fundacionpedromartinez.com", "Masiel", "Gomez", "SuperUser", shop);
            //    await CheckUser("RamonContreras@fundacionpedromartinez.com", "Ramon", "Contreras", "SuperUser", shop);
            //    await CheckUser("YadilJimenez@fundacionpedromartinez.com", "Yadil", "Jimenez", "SuperUser", shop);
                
            //}

            //if (!_context.Countries.Any())
            //{
            //    AddCountry("Republica Dominicana", "_Dominican@");
            //    AddCountry("Venezuela", "Venezolan@");
            //    AddCountry("China", "Chino@");
            //    AddCountry("Colombia", "Colombian@");
            //    AddCountry("Haiti", "Haitian@");
            //    AddCountry("Usa", "American@");
            //    AddCountry("Otro", "Otro");
            //}

            //if (!_context.Ocupations.Any())
            //{
            //    AddOcupation("_N/A");
            //    AddOcupation("Otr@");
            //    AddOcupation("Vendedor(a)");
            //    AddOcupation("Independiente");
            //    AddOcupation("Ama de Casa");
            //    AddOcupation("Pintor(a)");
            //    AddOcupation("Operario(a)");
            //    AddOcupation("Tecnico");
            //    AddOcupation("Que haceres del hogar");
            //    AddOcupation("Estudiante");
            //    AddOcupation("Quehaceres Domesticos");
            //    AddOcupation("Empleado(a) Privado");
            //    AddOcupation("Agricultor(a)");
            //    AddOcupation("Comerciante");
            //    AddOcupation("Politico(a)");
            //    AddOcupation("Chofer");
            //    AddOcupation("Empleado(a) Publico");
            //    AddOcupation("Empleado(a)");
            //    AddOcupation("Trabajador(a) Independiente");
            //    AddOcupation("Albañil");
            //    AddOcupation("Mecanico");
            //    AddOcupation("Licenciado(a)");
            //    AddOcupation("Operario(a)");
            //    AddOcupation("Maestro(a)");
            //    AddOcupation("Estilista");
            //    AddOcupation("Jubilado(a)/Pensionado(a)");
            //    AddOcupation("Secretario(a)");
            //    AddOcupation("Ebanista");
            //    AddOcupation("Chiripero(a)");
            //    AddOcupation("Ingeniero(a)");
            //    AddOcupation("Modista");
            //    AddOcupation("Administrador(a)");
            //    AddOcupation("Asignaturador(a)");
            //    AddOcupation("Abogado(a)");
            //    AddOcupation("Cominicador(a)");
            //    AddOcupation("Enfermero(a)");
            //    AddOcupation("Carpintero");
            //    AddOcupation("Doctor(a)");
            //    AddOcupation("Otras");
            //    _context.SaveChanges();

            //}

            //if (!_context.Religions.Any())
            //{
            //    AddReligion("_N/A (Desconocida)");
            //    AddReligion("Catolic@");
            //    AddReligion("Evangelic@");
            //    AddReligion("Cristian@");
            //    AddReligion("Adventista");
            //    AddReligion("Testigo de Jehova");
            //    AddReligion("Mormon");
            //    AddReligion("Judio");
            //    AddReligion("Musulman");
            //    AddReligion("Hinduista");
            //    AddReligion("Budista");
            //    AddReligion("Taoista");
            //    AddReligion("Confusionista");
            //    AddReligion("Shintoista");
            //    AddReligion("Ate@");
            //    AddReligion("Otr@");

            //}

            //if (!_context.SchoolLevels.Any())
            //{
            //    AddSchoolLevel("N/A");
            //    AddSchoolLevel("Ninguno");
            //    AddSchoolLevel("Primaria");
            //    AddSchoolLevel("Secundaria");
            //    AddSchoolLevel("Tecnica");
            //    AddSchoolLevel("Universitaria");
            //    AddSchoolLevel("_N/A (Desconocida)");
            //    AddSchoolLevel("Maestria");
            //    AddSchoolLevel("Doctorado");

            //}

            //if (!_context.BloodTypes.Any())
            //{
            //    AddBlood("_Desconocido");
            //    AddBlood("O+");
            //    AddBlood("A+");
            //    AddBlood("B+");
            //    AddBlood("AB+");
            //    AddBlood("O-");
            //    AddBlood("A-");
            //    AddBlood("B-");
            //    AddBlood("AB-");

            //}

            //if (!_context.Insurances.Any())
            //{
            //    AddInsurance("_N/A", "_Privado/Desconocido o No Asegurado", "");
            //    AddInsurance("APS", "Compañia de Seguros APS, S.R.L.", "");
            //    AddInsurance("ASEMAP", "ARS ASEMAP", "");
            //    AddInsurance("BMI", "ARS BMI", "");
            //    AddInsurance("CMD", "ARS CMD (Colegio Médico Dominicano)", "");
            //    AddInsurance("Bupa", "Bupa Dominicana, S.A.", "");
            //    AddInsurance("xed", "xed", "");
            //    AddInsurance("COOPSEGUROS", "Cooperativa Nacional de Seguros, C por A", "C/Hnos. Deligne No. 156.  Gascue, Santo Domingo, R.D.  Tel.: (809) 682-6118 / 6313  Email:coop.seguros@verizon.net.do");
            //    AddInsurance("General de Seguros", "General de Seguros, S.A. ", "Av. Sarasota No. 39, Torre Sarasota Center,  Bella Vista, Apartado 2183  Santo Domingo, R.D.  Tel.: (809) 535-8888");
            //    AddInsurance("La Colonial", "La Colonial, S.A. ", "Av. Sarasota No. 75, Bella Vista  Santo Domingo, R.D.  Tel.: (809) 508-0707 / (809) 533-8969  Fax: (809) 508-0608  Email: luis.guerrero@lacolonial.com.do");
            //    AddInsurance("La Monumental", "La Monumental de Seguros, C por A ", "Max Henríquez Ureña No.79  Santo Domingo, R.D.  Tel.: (809) 686-4744 - Fax: (809) 685-5381  www.lamonumental.com.do");
            //    AddInsurance("Multiseguros Mehr", "Multiseguros Mehr, S.A. ", "Av. Lope de Vega, Torre Novocentro,  Piso 3, Suite C8C  Ensanche Naco, Santo Domingo, D.N.  Tel.: (809) 378-1820");
            //    AddInsurance("REHSA", "REHSA Compañía de Seguros y Reaseguros, S.A.", "Av. Gustavo Mejía Ricart No.8,   Edif. Angloamericana, 2do nivel,  El Millón, Santo Domingo, D.N.  Tel.: (809) 548-7171");
            //    AddInsurance("Ademi", "Seguros Ademi, S.A.", "Calle Madame Curie No. 21, La Esperilla   Santo Domingo, D.N.  Tel.: (809) 683-0203 ext.2616");
            //    AddInsurance("Patria", "Seguros Patria, S. A.", "Av. 27 de Febrero #215, Entre Ortega y Gasset y   Tiradentes, Ens. Naco, Santo Domingo, D.N.  Tel: (809) 547-1234 Reclam (809) 687-3151  1(809) 200-1160 - Fax: 809) 221-8128  Email: patria@verizon.net.do");
            //    AddInsurance("Reservas", "Seguros Reservas, S.A.", "Av. Jiménez Moya, esq Calle 4   Ens. La Paz, Santo Domingo, R.D.  Tel.: (809) 960-7200 - Fax: (809) 960-6148  Email: smahfoud@segbanreservas.com");
            //    AddInsurance("Universal", "Seguros Universal, S. A.", "Av. Winston Churchill 1100, Apartado 1052  Santo Domingo, R.D.  Tels.: (809)544-7200 / (809) 544-7968  Fax: (809) 544-7914 - Santiago: (809) 530-5282  Email: universal.seguros@codetel.net.do");
            //    AddInsurance("Humano", "Humano", "");
            //    AddInsurance("Palic", "Palic Salud", "");
            //    AddInsurance("xed", "xed", "");
            //    AddInsurance("Renacer", "Renacer", "");
            //    AddInsurance("Semma", "Semma", "");
            //    AddInsurance("Futuro", "Futuro", "");
            //    AddInsurance("Semunace", "Semunace", "");
            //    AddInsurance("Simag", "Simag", "");
            //    AddInsurance("SenasaC", "SENASA CONTRIBUTIVO", "");
            //    AddInsurance("UASD", "UASD", "");
            //    AddInsurance("GMA", "GMA", "");
            //    AddInsurance("Metasalud", "Metasalud", "");
            //    AddInsurance("ARL", "ARL", "");
            //    AddInsurance("xed", "xed", "");
            //    AddInsurance("SenasaSub", "SENASA SUBSIDIADO", "");
            //    AddInsurance("BanCentral", "BANCENTRAL", "");
            //    AddInsurance("Constitucion", "CONSTITUCION", "");
            //    AddInsurance("Primera", "PRIMERA ARS", "");
            //    AddInsurance("SaludS", "SALUD SEGURA", "");
            //    AddInsurance("Yunen", "ARS Yunen", "");
            //    AddInsurance("F.F.A.A.", "F.F.A.A.", "");
            //}

            //if (!_context.Tandas.Any())
            //{
            //    AddTanda("Matutina");
            //    AddTanda("Tarde");
            //    AddTanda("Nocturna");
            //    AddTanda("Medio Dia");

            //}

            //if (!_context.Origins.Any())
            //{
            //    AddOrigin("1", "Credito", "Ingreso/Ahorro/Activo");
            //    AddOrigin("-1", "Debito", "Gasto/Prestamo/Pasivo");

            //}

            //if (!_context.ServiceTypes.Any())
            //{
            //    AddServType("_Servicio");
            //    AddServType("Producto");
            //}

            //if (!_context.PaymentTypes.Any())
            //{
            //    AddPaymentType("Ef", "Efectivo");
            //    AddPaymentType("CR", "Credito");
            //    AddPaymentType("TC", "Tarjeta de Credito");
            //    AddPaymentType("Ch", "Cheque");
            //    AddPaymentType("Bn", "Bonos");
            //    AddPaymentType("OC", "Orden de Compra");
            //    AddPaymentType("On", "Online");
            //    AddPaymentType("TE", "Trasnferencia Electronica");
            //}

            //if (!_context.RelationTypes.Any())
            //{
            //    AddRelationType("Padre");
            //    AddRelationType("Madre");
            //    AddRelationType("Tutor");
            //    AddRelationType("Abuela");
            //    AddRelationType("Abuelo");
            //    AddRelationType("Tio");
            //    AddRelationType("Tia");
            //    AddRelationType("Primo");
            //    AddRelationType("Prima");
            //    AddRelationType("Amigo");
            //    AddRelationType("Amiga");
            //    AddRelationType("Padrastro");
            //    AddRelationType("Madrastra");
            //    AddRelationType("Otro");
            //}

            //if (!_context.Status.Any())
            //{

            //    AddStatus("_Activo", "ALL"); _context.SaveChanges();
            //    AddStatus("Inactivo", "ALL"); _context.SaveChanges();
            //    AddStatus("Egresado", "ALL"); _context.SaveChanges();
            //    AddStatus("Retirado", "ALL"); _context.SaveChanges();

            //    AddStatus("Requerida", "Requisition"); _context.SaveChanges();
            //    AddStatus("Aprobada", "Requisition"); _context.SaveChanges();
            //    AddStatus("Rechazada", "Rechazada"); _context.SaveChanges();

            //}
            //var user = await _userHelper.GetUserByEmailAsync("admin@fundacionpedromartinez.com");
            //if (!_context.Projects.Any())
            //{
            //    AddProject("Guitarra Pal Pueblo", user);
            //    AddProject("Peloteros Costuristas", user);
            //    AddProject("Programa 3", user);
            //    _context.SaveChanges();
            //}

            //if (!_context.Signatures.Any())
            //{
            //    var program = _context.Projects.FirstOrDefault();
            //    AddSignature("Musica", user, program);
            //    AddSignature("Canto", user, program);
            //}

            //if (!_context.Courses.Any())
            //{
            //    AddCourse("Pre-Primario", user); _context.SaveChanges();
            //    AddCourse("1.º pr Primer grado", user); _context.SaveChanges();
            //    AddCourse("2.º pr Segundo grado", user); _context.SaveChanges();
            //    AddCourse("3.º pr Tercer grado", user); _context.SaveChanges();
            //    AddCourse("4.º pr Cuarto grado", user); _context.SaveChanges();
            //    AddCourse("5.º pr Quinto grado", user); _context.SaveChanges();
            //    AddCourse("6.º pr Sexto grado", user); _context.SaveChanges();
            //    AddCourse("1.º br Primer grado", user); _context.SaveChanges();
            //    AddCourse("2.º br Segundo grado", user); _context.SaveChanges();
            //    AddCourse("3.º br Tercer grado", user); _context.SaveChanges();
            //    AddCourse("4.º br Cuarto grado", user); _context.SaveChanges();
            //    AddCourse("5.º br Quinto grado", user); _context.SaveChanges();
            //    AddCourse("6.º br Sexto grado", user); _context.SaveChanges();
            //}

            //if (!_context.Schools.Any())
            //{
            //    AddSchool("_N/A", user);
            //}

            //if (!_context.Subjects.Any())
            //{
            //    AddSubject("Materia 1", "Mt100", user);
            //    AddSubject("Materia 2", "Mt200", user);
            //    AddSubject("Materia AbC", "MtA", user);
            //}

            //if (!_context.DaysOfTheWeeks.Any())
            //{
            //    AddDayOfTheWeek("Lunes");
            //    AddDayOfTheWeek("Martes");
            //    AddDayOfTheWeek("Miercoles");
            //    AddDayOfTheWeek("Jueves");
            //    AddDayOfTheWeek("Viernes");
            //    AddDayOfTheWeek("Sabado");
            //    AddDayOfTheWeek("Domingo");
            //}

            //await _context.SaveChangesAsync();

        }

        private void AddSchool(string v, ApplicationUser user)
        {
            _context.Schools.Add(new School
            {
                CreatedUser = user,
                Name = v,
                CreatedBy = user.Id
            });
        }
        private void AddDayOfTheWeek(string v)
        {
            _context.DaysOfTheWeeks.Add(new DayOfTheWeek
            {
                Name = v
            });
        }
        private void AddSubject(string v, string v2, ApplicationUser user)
        {
            _context.Subjects.Add(new Subject
            {
                CreatedUser = user,
                Code = v2,
                Name = v,
                CreatedBy = user.Id
            });
        }
        private void AddCourse(string v, ApplicationUser user)
        {
            _context.Courses.Add(new Course
            {
                CreatedUser = user,
                Name = v,
                CreatedBy = user.Id
            });
        }
        private void AddProject(string v, ApplicationUser user)
        {
            _context.Projects.Add(new Project
            {
                CreatedUser = user,
                Name = v,
                CreatedBy = user.Id
            });
        }
        private void AddSignature(string v, ApplicationUser user, Project program)
        {
            _context.Signatures.Add(new Signature
            {
                Project = program,
                CreatedUser = user,
                Name = v,
                CreatedBy = user.Id
            });
        }
        private void AddStatus(string v, string v2)
        {
            _context.Status.Add(new Status
            {
                Name = v,
                Table = v2
            });
        }
        private void AddRelationType(string v)
        {
            _context.RelationTypes.Add(new RelationType
            {
                Name = v,
            });
        }
        private void AddPaymentType(string v1, string v2)
        {
            _context.PaymentTypes.Add(new PaymentType
            {
                Code = v1,
                Name = v2,
            });
        }
        private void AddServType(string v)
        {
            _context.ServiceTypes.Add(new ServiceType
            {
                Name = v,
            });
        }
        private void AddOrigin(string v1, string v2, string v3)
        {
            _context.Origins.Add(new Origin
            {
                Code = v1,
                Name = v2,
                Description = v3
            });
        }
        private void AddTanda(string name)
        {
            _context.Tandas.Add(new Tanda
            {
                Name = name,
            });
        }
        private void AddInsurance(string code, string name, string aditional)
        {
            _context.Insurances.Add(new Insurance
            {
                Code = code,
                Name = name,
                AditionalInfo = aditional
            });
        }
        private void AddBlood(string name)
        {
            _context.BloodTypes.Add(new BloodType
            {
                Code = name,
                Name = name
            });
        }
        private void AddSchoolLevel(string name)
        {
            _context.SchoolLevels.Add(new SchoolLevel
            {
                Name = name
            });
        }
        private void AddCountry(string name, string denomyn)
        {
            _context.Countries.Add(new Country
            {
                Name = name,
                Denomym = denomyn
            });
        }
        private void AddOcupation(string name)
        {
            _context.Ocupations.Add(new Ocupation
            {
                Name = name
            });
        }
        private void AddReligion(string name)
        {
            _context.Religions.Add(new Religion
            {
                Name = name
            });
        }

        private async Task<ApplicationUser> CheckUser(string userName, string firstName, string lastName, string role, Shop shop)
        {
            // Add user
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                user = await AddUser(userName, firstName, lastName, role, shop);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            await _userHelper.AddClaim(user, new Claim("OwnerId", shop.Owner.Id.ToString()));
            await _userHelper.AddClaim(user, new Claim("ShopId", shop.Id.ToString()));


            return user;
        }

        private async Task<ApplicationUser> AddUser(string userName, string firstName, string lastName, string role, Shop shop)
        {
            var user = new ApplicationUser
            {
                Name = firstName,
                Lastname = lastName,
                Email = userName,
                UserName = userName,
                PhoneNumber = "849 207 7714",
                Shop = shop
                //CityId = context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                //City = context.Countries.FirstOrDefault().Cities.FirstOrDefault()
            };

            var result = await _userHelper.AddUserAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await _userHelper.AddUserToRoleAsync(user, role);
            var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);
            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("User");
            await _userHelper.CheckRoleAsync("Supervisor");
            await _userHelper.CheckRoleAsync("SuperUser");
         
        }

    }
}
