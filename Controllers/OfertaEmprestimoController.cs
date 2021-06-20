using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using Microsoft.Extensions.Configuration;
using System;

namespace API_Emprestimos.Controllers
{
    public class OfertaEmprestimoController : EntityController<OfertaEmprestimo, OfertaEmprestimoRepository>
    {
        public OfertaEmprestimoController(IConfiguration configuration, IServiceProvider serviceProvider, OfertaEmprestimoRepository repository)
            : base(configuration, serviceProvider, repository)
        {
        }
    }
}
