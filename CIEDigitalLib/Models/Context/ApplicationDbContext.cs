using CIEDigitalLib.Models.Binding;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CIEDigitalLib.Models.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}