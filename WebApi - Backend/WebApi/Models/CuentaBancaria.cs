namespace WebApi.Models
{
    public class CuentaBancaria
    {
        public Guid Id { get; set; }

        public int? IdUser { get; set; }

        public int? MontoActual { get; set; }

        public int? NumeroCuenta { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }



    }


    public class CuentaBancariaDTO
    {
        public int? NumeroCuenta { get; set; }

        public int? IdTipoAccion { get; set; }

        public int? Monto { get; set; }

    }
}
