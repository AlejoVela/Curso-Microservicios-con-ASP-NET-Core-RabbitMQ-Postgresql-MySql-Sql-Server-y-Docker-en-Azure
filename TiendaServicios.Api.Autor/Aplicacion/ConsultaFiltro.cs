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
    public class ConsultaFiltro
    {
        public class AutorUnico: IRequest<AutorDTO>
        {
            public string AutorGuid { get; set; }
        }
        public class Manejador : IRequestHandler<AutorUnico, AutorDTO>
        {
            public ContextoAutor _context;
            public IMapper _mapper;

            public Manejador(ContextoAutor context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibro.Where(x =>
                    x.AutorLibroGuid == request.AutorGuid
                )
                .FirstOrDefaultAsync();
                if (autor == null)
                {
                    throw new Exception("No se encontro el autor");
                }

                var autorDTO = _mapper.Map<AutorLibro, AutorDTO>(autor);

                return autorDTO;
            }
        }
    }
}
