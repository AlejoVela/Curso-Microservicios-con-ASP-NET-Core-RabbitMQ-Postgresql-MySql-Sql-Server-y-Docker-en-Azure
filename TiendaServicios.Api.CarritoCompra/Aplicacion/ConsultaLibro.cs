using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class ConsultaLibro
    {
        public class Ejecuta : IRequest<CarritoDTO>
        {
            public int LibroSesionId { get; set; }
        }
        //primer parametro, desde que clase se procensan 
        //los parametros de entrada, el segundo el tipo de 
        //dato/objeto a devolver
        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibrosService _libroService;

            public Manejador(CarritoContexto contexto, ILibrosService libroService)
            {
                _contexto = contexto;
                _libroService = libroService;
            }
            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.LibroSesionId);
                //devuelve la lista de IDs de los productos
                var carrotoSesionDetalle = await _contexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.LibroSesionId).ToListAsync();
                //creamos una lista que almacenará la lista de productos que obtengamos}
                var listaCarrito = new List<CarritoDetalleDTO>();
                foreach (var libro in carrotoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if(response.Resultado)
                    {
                        var objetoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDTO
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId
                        };
                        listaCarrito.Add(carritoDetalle);
                    }
                }

                var carritoSesionDetalle = new CarritoDTO
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarrito
                };

                return carritoSesionDetalle;
            }
        }
    }
}
