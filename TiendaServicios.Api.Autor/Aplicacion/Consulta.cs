using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicación
{
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDTO>>
        {

        }
        public class Manejador : IRequestHandler<ListaAutor, List<AutorDTO>>
        {
            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }
            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await _context.AutorLibro.ToListAsync();
                var autoresDTO = this._mapper.Map<List<AutorLibro>, List<AutorDTO>>(autores);

                return autoresDTO;
            }
        }
    }
}
