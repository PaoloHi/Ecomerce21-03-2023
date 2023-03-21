using EcommerceRealCVO.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceRealCVO.Datos
{
    public class AplicationDBContext : IdentityDbContext
    {
        public AplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUsuario> AppUsuarios { get; set; }

    }


}
