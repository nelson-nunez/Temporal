using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Model;

namespace IntuitChallengeAPI.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class SocioController : Controller
    {
        private readonly ICsvReaderService _csvReaderService;

        // Inyectar dependencias
        public SocioController(ICsvReaderService csvReaderService)
        {
            try
            {
                _csvReaderService = csvReaderService;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #region GET

        [HttpGet("")]
        public ActionResult<List<Socio>> GetSociosPorNombreAsync([FromQuery] string nombre)
        {
            List<Socio> socios;
            try
            {
                socios = _csvReaderService.ReadCsvFromFile();
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest($"Error de formato en el archivo CSV: {ex.Message}");
            }

            if (!string.IsNullOrWhiteSpace(nombre) && nombre != "todos")
            {
                socios = socios.Where(x => x.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Ok(socios);
        }
        #endregion
    }
}


