using Microsoft.EntityFrameworkCore;
using PaginaColetaEmailVideo.Models;

namespace PaginaColetaEmailVideo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<EmailModel> Emails { get; set; }
    }
}
