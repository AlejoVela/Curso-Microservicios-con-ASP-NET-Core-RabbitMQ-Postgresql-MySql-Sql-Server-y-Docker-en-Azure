using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibroMaterialController (IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }
        [HttpGet]
        public async Task<ActionResult<List<LibroMaterialDTO>>> GetLibro()
        {
            return await _mediator.Send(new Consulta.Ejecuta());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDTO>> GetLibroUnico(Guid id)
        {
            return await _mediator.Send(new ConsultaLibroId.LibroUnico { LibroId = id });
        }
        [HttpGet("Autor/{idAutor}")]
        public async Task<ActionResult<List<LibroMaterialDTO>>> GetLibroByAutorId(Guid idAutor)
        {
            return await _mediator.Send(new ConsultaLibroIdAutor.LibroPorAutor { AutorId = idAutor });
        }
    }
}
