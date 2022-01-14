using WebApi.DataContext;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ITransferenciaBancariaService
    {
        List<TransferenciaBancaria> GetbyId(int numeroCuenta);

        void postTransaccion(TransferenciaBancariaDTO transferenciaBancaria);

        bool updateAmount(TransferenciaBancariaDTO transferenciaBancaria);

    }
    public class TransaccionesBancariasService : ITransferenciaBancariaService
    {
        private readonly AppDbContext _context;

        public TransaccionesBancariasService (AppDbContext context)
        {
            _context = context;
        }
        public List<TransferenciaBancaria> GetbyId(int numeroCuenta)
        {
            return _context.TransferenciaBancarias.Where(x => x.NumeroCuentaOrigen == numeroCuenta).ToList();
        }

        public void postTransaccion(TransferenciaBancariaDTO transferenciaBancaria)
        {
            var addTransfer = new TransferenciaBancaria { 
                NumeroCuentaDestino = transferenciaBancaria.NumeroCuentaDestino, 
                NumeroCuentaOrigen = transferenciaBancaria.NumeroCuentaOrigen, 
                IdTipoTransaccion = transferenciaBancaria.IdTipoTransaccion,
                MontoTransaccion = transferenciaBancaria.MontoTransaccion,
                FechaCreacion = DateTime.Now 
            };
            _context.TransferenciaBancarias.Add(addTransfer);
            _context.SaveChanges();
        }

        public bool updateAmount(TransferenciaBancariaDTO transferenciaBancaria)
        {
            var accountOrigen = _context.CuentaBancarias.Where(x => x.NumeroCuenta == transferenciaBancaria.NumeroCuentaOrigen).FirstOrDefault();
            var accountDestino = _context.CuentaBancarias.Where(x => x.NumeroCuenta == transferenciaBancaria.NumeroCuentaDestino).FirstOrDefault();

            if(accountOrigen != null && accountDestino != null)
            {
                accountOrigen.MontoActual = accountOrigen.MontoActual - transferenciaBancaria.MontoTransaccion;
                accountDestino.MontoActual = transferenciaBancaria.MontoTransaccion;
                _context.SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }
    }
}
