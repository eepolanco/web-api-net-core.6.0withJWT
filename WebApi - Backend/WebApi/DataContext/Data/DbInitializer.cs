using Microsoft.EntityFrameworkCore;

namespace WebApi.DataContext.Data
{
    public static class DbInitializer
    {
        public static void Initialize (IServiceProvider serviceProvider)
        {
            using (var _context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                //Agregando cuentas Bancarias
                if(_context.CuentaBancarias.Any())
                {
                    return;
                }

                _context.CuentaBancarias.Add(
                    new Models.CuentaBancaria { IdUser = 1, MontoActual = 10000, NumeroCuenta = 12345678, FechaCreacion = DateTime.Now }
                );

                _context.SaveChanges();

                //Agregando user by Default
                if (_context.Users.Any())
                {
                    return;
                }

                _context.Users.AddRange(
                    new Entities.User { Id = 1, FirstName = "ADMIN", LastName = "ADMIN", Username = "admin", Password = "admin123" }

                );

                _context.SaveChanges();


                //Agregando Transacciones
                if (_context.Users.Any())
                {
                    return;
                }

                _context.TransferenciaBancarias.AddRange(
                    new Models.TransferenciaBancaria { IdTipoTransaccion = 1, MontoTransaccion = 100000, NumeroCuentaOrigen = 12345678, NumeroCuentaDestino = 12345679 }

                );

                _context.SaveChanges();
            }
        }
    }
}
