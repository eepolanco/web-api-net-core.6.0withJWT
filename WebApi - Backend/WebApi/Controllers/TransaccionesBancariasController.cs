using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesBancariasController : ControllerBase
    {
        private ITransferenciaBancariaService _transferenciaBancariaService;

        public TransaccionesBancariasController(ITransferenciaBancariaService transferenciaBancariaService)
        {
            _transferenciaBancariaService = transferenciaBancariaService;
        }

        [HttpGet("{numeroCuentaDestino}")]
        public IActionResult Get(int numeroCuentaDestino)
        {
            try
            {
                var transferencias = _transferenciaBancariaService.GetbyId(numeroCuentaDestino);
                return Ok(transferencias);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] TransferenciaBancariaDTO transferenciaBancariaDTO)
        {
            try
            {
                _transferenciaBancariaService.updateAmount(transferenciaBancariaDTO);
                _transferenciaBancariaService.postTransaccion(transferenciaBancariaDTO);

                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
