using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace API_Emprestimos.Controllers
{
    public class AceiteEmprestimoController : EntityController<AceiteEmprestimo, AceiteEmprestimoRepository>
    {
        public AceiteEmprestimoController(IConfiguration configuration, IServiceProvider serviceProvider, AceiteEmprestimoRepository repository, ContextoExecucao contexto)
            : base(configuration, serviceProvider, repository, contexto)
        {
        }

        [HttpGet]
        public List<AceiteEmprestimo> Get()
        {
            return repository.GetUsuario();
        }
    }
}
