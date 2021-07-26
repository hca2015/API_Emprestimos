using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using Microsoft.Extensions.Configuration;
using System;

namespace API_Emprestimos.Controllers
{
    public class AceiteEmprestimoController : EntityController<AceiteEmprestimo, AceiteEmprestimoRepository>
    {
        public AceiteEmprestimoController(IConfiguration configuration, IServiceProvider serviceProvider, AceiteEmprestimoRepository repository, ContextoExecucao contexto)
            : base(configuration, serviceProvider, repository, contexto)
        {
        }
    }
}
