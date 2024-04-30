using Microsoft.EntityFrameworkCore;
using WebAPICRUD_Escola.Model;

namespace WebAPICRUD_Escola.DataContext
{
    public class ApplicationDbContaxt:DbContext
    {
        public ApplicationDbContaxt(DbContextOptions<ApplicationDbContaxt> options) : base(options)
        {
            
        }
        public DbSet<EscolaModel> Escolas { get; set; }
        public DbSet<ProvinciaModel> Provincias { get; set; }
    }
}
