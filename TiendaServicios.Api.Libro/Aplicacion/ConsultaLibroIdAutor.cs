using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaLibroIdAutor
    {
        public class LibroPorAutor : IRequest<List<LibroMaterialDTO>>
        {
            public Guid AutorId { get; set; }
        }
        public class Manejador : IRequestHandler<LibroPorAutor, List<LibroMaterialDTO>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador (ContextoLibreria contexto, IMapper mapper)
            {
                this._contexto = contexto;
                this._mapper = mapper;
            }
            public async Task<List<LibroMaterialDTO>> Handle(LibroPorAutor request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.Where(x => x.AutorLibro == request.AutorId).ToListAsync();
                var librosDTO = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDTO>>(libros);
                return librosDTO;
            }
        }
    }
}
