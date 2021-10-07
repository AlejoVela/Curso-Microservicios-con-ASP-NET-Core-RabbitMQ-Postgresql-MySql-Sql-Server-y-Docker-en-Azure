using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }
        public DateTime? FechaCreacion { get; set; }

        /// <summary>
        /// esta propiedad indica que se manejaran una coleccion de CarritoSesiónDetalle, es decir,
        /// Es la relación que indica que un carrito Sesión, puede contener varios carritoSesiónDetalle
        /// </summary>
        public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; }
    }
}
