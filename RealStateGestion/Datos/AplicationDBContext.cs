using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealStateGestion.Models;

namespace RealStateGestion.Datos
{
    public class AplicationDBContext : IdentityDbContext
    {
        public AplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUsuario> AppUsuarios { get; set; }

    }
}
