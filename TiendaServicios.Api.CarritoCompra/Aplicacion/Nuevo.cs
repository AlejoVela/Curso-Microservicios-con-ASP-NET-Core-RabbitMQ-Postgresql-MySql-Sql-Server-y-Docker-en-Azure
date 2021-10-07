using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly CarritoContexto _contexto;
            public Manejador(CarritoContexto contexto)
            {
                this._contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                _contexto.CarritoSesion.Add(carritoSesion);
                var result = await _contexto.SaveChangesAsync();
                if (result == 0) throw new Exception("No se pudo insertar carrito de compras");

                int id = carritoSesion.CarritoSesionId; //obtenemos el id de la sesión

                foreach (var obj in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = obj
                    };
                    _contexto.CarritoSesionDetalle.Add(detalleSesion);
                }
                result = await _contexto.SaveChangesAsync();

                if (result < 1) throw new Exception("No se pudo insertar carrito de compras");

                return Unit.Value;
            }
        }
    }
}
