namespace WebApi.Services;

using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.DataContext;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using BCryptNet = BCrypt;

public interface ICuentaBancariaService
{
    List<CuentaBancaria> GetAll();

    List<CuentaBancaria> GetbyId(int id);

    void postCuentaBancaria(int idUser);

    void updateAmount(CuentaBancariaDTO data);

}

public class CuentaBancariaService : ICuentaBancariaService

{
    private readonly AppDbContext _context;

    public CuentaBancariaService( AppDbContext context)
    {
        _context = context;
    }

    public List<CuentaBancaria> GetbyId(int id)
    {
        return _context.CuentaBancarias.Where(x => x.IdUser == id).ToList();
    }

    public List<CuentaBancaria> GetAll()
    {
        return _context.CuentaBancarias.ToList();
    }

    public void postCuentaBancaria(int idUser)
    {
        Random rnd = new Random();
        int myRandomACcount = rnd.Next(10000000, 99999999);
        var addAccount = new CuentaBancaria { IdUser = idUser, MontoActual = 0, NumeroCuenta = myRandomACcount, FechaCreacion = DateTime.Now };
        _context.CuentaBancarias.Add(addAccount);
        _context.SaveChanges();

    }

    public void updateAmount(CuentaBancariaDTO data)
    {
        var account = _context.CuentaBancarias.Where(x => x.NumeroCuenta == data.NumeroCuenta).FirstOrDefault();

        if (data.IdTipoAccion == 1)
        { account.MontoActual = account.MontoActual + data.Monto; }
        else
        { account.MontoActual = account.MontoActual - data.Monto; }

        _context.SaveChanges();
    }
}