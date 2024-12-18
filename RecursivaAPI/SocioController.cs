using Microsoft.AspNetCore.Mvc;
using Model;

namespace RecursivaAPI
{
    

        [Route("[Controller]")]
        [ApiController]
        public class SocioController : Controller
        {
            private readonly ICSVReader _csvReaderService;

            // Inyectar dependencias
            public SocioController(ICSVReader csvReaderService)
            {
                _csvReaderService = csvReaderService;
            }

            #region GET

            /// <summary> 
            /// 1.Cantidad total de personas registradas.
            /// </summary>        
            [HttpGet("Total")]
            public ActionResult<List<Socio>> Get_TotalSocios()
            {
                var total = 0;
                List<Socio> socios = _csvReaderService.ReadCsvFromFile();

                if (socios != null & socios.Any())
                    total = socios.Count();

                return Ok(total);
            }

            /// <summary> 
            /// 2. El promedio de edad de los socios de Racing
            /// </summary>
            [HttpGet("Promedio")]
            public ActionResult<List<Socio>> Get_PromedioEdadSociosRacing()
            {
                double promedio = 0;
                List<Socio> socios = _csvReaderService.ReadCsvFromFile();

                if (socios != null && socios.Any())
                {
                    var sociosRacing = socios.Where(x => x.Equipo.ToLower() == "racing").ToList();
                    if (sociosRacing.Any())
                    {
                        var edadTot = sociosRacing.Sum(x => x.Edad);
                        var cantSocios = sociosRacing.Count();
                        promedio = (double)edadTot / cantSocios;
                    }
                }
                return Ok(promedio);
            }

            /// <summary>
            /// 3. Un listado con las 100 primeras personas casadas, con estudios Universitarios,
            /// ordenadas de menor a mayor según su edad. 
            /// Por cada persona, mostrar: nombre, edad y equipo.
            /// </summary>
            [HttpGet("100")]
            public ActionResult<List<SocioDTO>> Get_100Socios()
            {
                List<Socio> socios = new List<Socio>();
                List<SocioDTO> listadoDTO = new List<SocioDTO>();

                socios = _csvReaderService.ReadCsvFromFile();

                if (socios != null && socios.Any())
                {
                    listadoDTO = socios
                        .Where(x => x.EstadoCivil.ToLower() == "casado" && x.NivelDeEstudios.ToLower() == "universitario") // Filtramos por estado civil y nivel de estudios
                        .Take(100)
                        .OrderBy(x => x.Edad)
                        .Select(x => new SocioDTO(x.Nombre, x.Edad, x.Equipo))
                        .ToList();
                }

                return Ok(listadoDTO);
            }

            /// <summary> 
            /// 4. Un listado con los 5 nombres más comunes entre los hinchas de River.
            /// </summary>
            [HttpGet("Top5NombresRiver")]
            public ActionResult<List<string>> Get_Top5NombresRiver()
            {
                List<Socio> socios = _csvReaderService.ReadCsvFromFile();
                var top5Nombres = new List<string>();

                if (socios != null && socios.Any())
                {
                    top5Nombres = socios
                        .Where(x => x.Equipo.ToLower() == "river")
                        .GroupBy(x => x.Nombre)
                        .OrderByDescending(g => g.Count())
                        .Take(5)
                        .Select(g => g.Key)
                        .ToList();
                }

                return Ok(top5Nombres);
            }

            /// <summary> 
            /// 5. Un listado, ordenado de mayor a menor según la cantidad de socios, que enumere, 
            /// junto con cada equipo, el promedio de edad de sus socios, la menor edad registrada 
            /// y la mayor edad registrada.
            /// </summary>
            [HttpGet("EstadisticasEquipos")]
            public ActionResult<List<Estadisticas>> Get_EstadisticasEquipos()
            {
                List<Socio> socios = _csvReaderService.ReadCsvFromFile();
                var estadisticasEquipos = new List<Estadisticas>();

                if (socios != null && socios.Any())
                {
                    estadisticasEquipos = socios
                        .GroupBy(x => x.Equipo)
                        .Select(g => new Estadisticas
                        {
                            Equipo = g.Key,
                            CantidadSocios = g.Count(),
                            PromedioEdad = g.Average(x => x.Edad),
                            EdadMinima = g.Min(x => x.Edad),
                            EdadMaxima = g.Max(x => x.Edad)
                        })
                        .OrderByDescending(x => x.CantidadSocios)
                        .ToList();
                }

                return Ok(estadisticasEquipos);
            }

            #endregion
        }
    
}
