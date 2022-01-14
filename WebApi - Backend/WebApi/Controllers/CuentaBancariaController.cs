using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaBancariaController : ControllerBase
    {

        private ICuentaBancariaService _cuentaBancariaService;


        public CuentaBancariaController(ICuentaBancariaService cuentaBancariaService)
        {
            _cuentaBancariaService = cuentaBancariaService;
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var cuentas = _cuentaBancariaService.GetAll();
                return Ok(cuentas);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var accountList = _cuentaBancariaService.GetbyId(id);
                return Ok(accountList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] int idUser)
        {
            try
            {
                _cuentaBancariaService.postCuentaBancaria(idUser);
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        [HttpPut]
        public IActionResult Put([FromBody] CuentaBancariaDTO data)
        {

            try
            {
                _cuentaBancariaService.updateAmount(data);
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
