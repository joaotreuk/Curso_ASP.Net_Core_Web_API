using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=ProAgil.db");

        public DbSet<Evento> Eventos { get; set; }
    }
}