using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibroService : ILibrosService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibroService> _logger;
        public LibroService (IHttpClientFactory httpClient, ILogger<LibroService> logger)
        {
            this._httpClient = httpClient;
            this._logger = logger;
        }
        public async Task<(bool Resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var cliente = _httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/LibroMaterial/{LibroId}");
                if (!response.IsSuccessStatusCode)
                {
                    return (false, null, response.ReasonPhrase);
                }

                var contenido = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                return (true, resultado, null);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
