using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public override string Id
        {
            get
            {
                return base.Id;
            }

            set
            {
                base.Id = value;
            }
        }
        public override string PasswordHash
        {
            get
            {
                return base.PasswordHash;
            }

            set
            {
                base.PasswordHash = value;
            }
        }
        [ScaffoldColumn(false)]
        public int LevelId { get; set; }
        public virtual LevelModel Level { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(x => x.Id).HasColumnName("UserId");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(x => x.Id).HasColumnName("RoleId");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(x => x.Name).HasColumnName("RoleName");
            //modelBuilder.Entity<IdentityUser>().HasMany<IdentityUserRole>((IdentityUser u) => u.Roles );
        }
        public static ApplicationDbContext Create()
        {            
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebApplication5.Models.LevelModel> LevelModels { get; set; }

        public System.Data.Entity.DbSet<WebApplication5.Models.DifficultyModel> DifficultyModels { get; set; }
        public bool Seed(ApplicationDbContext context)
        {
            bool success = true;
            context.Roles.Add(new IdentityRole("Admin") );
            context.Roles.Add(new IdentityRole("Users"));
            context.SaveChanges();
                
            context.DifficultyModels.Add(new DifficultyModel() { Description = "Easy" });
            context.DifficultyModels.Add(new DifficultyModel() { Description = "Normal" });
            context.DifficultyModels.Add(new DifficultyModel() { Description = "Hard" });
            context.SaveChanges();
            context.LevelModels.Add(new LevelModel() { Question = "What is the key", Answer = "AAbbccddffgg", DifficultyId = 1, Hint = "Answer is in this page" });
            context.SaveChanges();
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var adminUser = new ApplicationUser() { UserName = "Admin", LevelId = 1 };
            userManager.Create(adminUser, "Password@123");
            userManager.AddToRole(adminUser.Id, "Admin");
            context.SaveChanges();
            return success;
        }
        public class DropCreateAlwaysInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }
    }
   
}