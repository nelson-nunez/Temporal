using Model;

namespace RecursivaUI.Services
{
    public class SocioService
    {
        private readonly HttpClient _httpClient;

        public SocioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44397");
        }

        // 1. Obtener la cantidad total de socios registrados
        public async Task<int> GetTotalSociosAsync()
        {
            var response = await _httpClient.GetAsync("/Socio/Total");
            if (response.IsSuccessStatusCode)
            {
                var total = await response.Content.ReadFromJsonAsync<int>();
                return total;
            }
            else
            {
                throw new Exception("Error al obtener el total de socios");
            }
        }

        // 2. Obtener el promedio de edad de los socios de Racing
        public async Task<double> GetPromedioEdadSociosRacingAsync()
        {
            var response = await _httpClient.GetAsync("/Socio/Promedio");
            if (response.IsSuccessStatusCode)
            {
                var promedio = await response.Content.ReadFromJsonAsync<double>();
                return promedio;
            }
            else
            {
                throw new Exception("Error al obtener el promedio de edad de los socios de Racing");
            }
        }

        // 3. Obtener los primeros 100 socios casados con estudios universitarios, ordenados por edad
        public async Task<List<SocioDTO>> Get100SociosAsync()
        {
            var response = await _httpClient.GetAsync("/Socio/100");
            if (response.IsSuccessStatusCode)
            {
                var socios = await response.Content.ReadFromJsonAsync<List<SocioDTO>>();
                return socios;
            }
            else
            {
                throw new Exception("Error al obtener los primeros 100 socios");
            }
        }

        // 4. Obtener los 5 nombres más comunes entre los hinchas de River
        public async Task<List<string>> GetTop5NombresRiverAsync()
        {
            var response = await _httpClient.GetAsync("/Socio/Top5NombresRiver");
            if (response.IsSuccessStatusCode)
            {
                var nombres = await response.Content.ReadFromJsonAsync<List<string>>();
                return nombres;
            }
            else
            {
                throw new Exception("Error al obtener los 5 nombres más comunes entre los hinchas de River");
            }
        }

        // 5. Obtener estadísticas de los equipos (promedio de edad, edad mínima, edad máxima, cantidad de socios)
        public async Task<List<Estadisticas>> GetEstadisticasEquiposAsync()
        {
            var response = await _httpClient.GetAsync("/Socio/EstadisticasEquipos");
            if (response.IsSuccessStatusCode)
            {
                var estadisticas = await response.Content.ReadFromJsonAsync<List<Estadisticas>>();
                return estadisticas;
            }
            else
            {
                throw new Exception("Error al obtener las estadísticas de los equipos");
            }
        }
    }
}

