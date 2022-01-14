namespace WebApi.Models
{
    public class TransferenciaBancaria
    {
        public Guid Id { get; set; }

        public int? NumeroCuentaOrigen { get; set; }

        public int? NumeroCuentaDestino { get; set; }

        public int? IdTipoTransaccion { get; set; }

        public int? MontoTransaccion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }


    public class TransferenciaBancariaDTO
    {

        public int? NumeroCuentaOrigen { get; set; }

        public int? NumeroCuentaDestino { get; set; }

        public int? IdTipoTransaccion { get; set; }

        public int? MontoTransaccion { get; set; }

    }
}
