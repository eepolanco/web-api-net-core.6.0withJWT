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

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    void Register(RegisterRequest model);
    IEnumerable<User> GetAll();
    User GetById(int id);
}

public class UserService : IUserService

{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    //private List<User> _users = new List<User>
    //{
    //    new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test123" }
    //};

    private readonly AppDbContext _context;
    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings, AppDbContext context)
    {
        _appSettings = appSettings.Value;
        _context = context;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
        //var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public void Register(RegisterRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.Username == model.Username))
            throw new InvalidOperationException("Username '" + model.Username + "' is already taken");

        //// map model to new user object
        //var user = _mapper.Map<User>(model);
        //int x = generateAleatoryAccount();
        Random rnd = new Random();
        int myRandomACcount = rnd.Next(10000000, 99999999);

        var lastUser = _context.Users.LastOrDefault();
        var user = new User { Id = lastUser.Id + 1, FirstName = model.FirstName, LastName = model.LastName, Username = model.Username, Password = model.Password };
        var addAccount = new CuentaBancaria { IdUser = lastUser.Id + 1, MontoActual = 0, NumeroCuenta = myRandomACcount, FechaCreacion = DateTime.Now };



        // save user
        _context.Users.Add(user);
        _context.CuentaBancarias.Add(addAccount);

        _context.SaveChanges();
    }

  


    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User GetById(int id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}