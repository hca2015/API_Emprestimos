using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using Microsoft.Extensions.Configuration;
using System;

namespace API_Emprestimos.Controllers
{
    public class UsuarioController : EntityController<Usuario, UsuarioRepository>
    {
        public UsuarioController(IConfiguration configuration, IServiceProvider serviceProvider, UsuarioRepository repository, ContextoExecucao contexto)
            : base(configuration, serviceProvider, repository, contexto)
        {
        }
    }
}
