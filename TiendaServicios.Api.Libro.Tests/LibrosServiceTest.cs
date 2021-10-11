using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        [Fact]
        public void GetLibros()
        {
            //Debemos emular a los objetos inyectados dentro de nuestras clases a probar
            //en este caso, probaremos la clase nuevo-manejador del proyecto libro
            //1. Emular a la instancia de EntityFramework core - ContextoLibreria
            //para emular las acciones y eventos de un objeto de ambiente de unit test
            //utilizamos objetos de tipo Mock
            var mockContexto = new Mock<ContextoLibreria>();

            //2. Emulamos tampien a un objeto IMapper
            var mockMapper = new Mock<IMapper>();

            //3. Intanciar a la clase manejador y pasar como parametros los mocks creados
            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mockMapper.Object);
        }
    }
}
