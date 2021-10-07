using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoSesionDetalle
    {
        public int CarritoSesionDetalleId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string ProductoSeleccionado { get; set; }
        /// <summary>
        /// carrito sesión ID nos permite guardar dentro de la tabla "CarritoSesionDetalle" el id del
        /// carritoSesion al que pertenece, pero para esto es necesario tambien crear un objeto que representa la 
        /// relación, de ahi que se cree después un objeto de tipo "CarritoSesion" como ancla
        /// </summary>
        public int CarritoSesionId { get; set; }
        public CarritoSesion CarritoSesion { get; set; }
    }
}
