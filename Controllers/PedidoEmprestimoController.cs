using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using Microsoft.Extensions.Configuration;
using System;

namespace API_Emprestimos.Controllers
{
    public class PedidoEmprestimoController : EntityController<PedidoEmprestimo, PedidoEmprestimoRepository>
    {
        public PedidoEmprestimoController(IConfiguration configuration, IServiceProvider serviceProvider, PedidoEmprestimoRepository repository)
            : base(configuration, serviceProvider, repository)
        {
        }
    }
}
