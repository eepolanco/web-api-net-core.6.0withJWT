using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
        }

        public DbSet<CuentaBancaria> CuentaBancarias { get; set; }
        public DbSet<TransferenciaBancaria> TransferenciaBancarias { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
