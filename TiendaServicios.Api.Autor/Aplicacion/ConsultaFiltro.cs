﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicación
{
    public class ConsultaFiltro
    {
        public class AutorUnico: IRequest<AutorLibro>
        {
            public string AutorGuid { get; set; }
        }
        public class Manejador : IRequestHandler<AutorUnico, AutorLibro>
        {
            public ContextoAutor _context;

            public Manejador (ContextoAutor context)
            {
                this._context = context;
            }

            public async Task<AutorLibro> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibro.Where(x =>
                    x.AutorLibroGuid == request.AutorGuid
                )
                .FirstOrDefaultAsync();
                if (autor == null)
                {
                    throw new Exception("No se encontro el autor");
                }

                return autor;
            }
        }
    }
}