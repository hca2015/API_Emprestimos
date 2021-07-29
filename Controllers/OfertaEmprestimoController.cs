using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using API_Emprestimos.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace API_Emprestimos.Controllers
{
    public class OfertaEmprestimoController : EntityController<OfertaEmprestimo, OfertaEmprestimoRepository>
    {
        private readonly CancelarOfertaService cancelarOfertaService;
        private readonly AceitarOfertaService aceitarOfertaService;

        public OfertaEmprestimoController(IConfiguration configuration,
            IServiceProvider serviceProvider,
            OfertaEmprestimoRepository repository,
            ContextoExecucao contexto
            , CancelarOfertaService cancelarOfertaService
            , AceitarOfertaService aceitarOfertaService)
            : base(configuration, serviceProvider, repository, contexto)
        {
            this.cancelarOfertaService = cancelarOfertaService;
            this.aceitarOfertaService = aceitarOfertaService;
        }

        [HttpGet]
        public List<OfertaEmprestimo> Get()
        {
            return repository.GetUsuario();
        }

        [HttpPost("Cancelar")]
        public IActionResult Cancelar(OfertaEmprestimo oferta)
        {
            if (oferta == null)
                return BadRequest();

            cancelarOfertaService.Cancelar(oferta.OFERTAID);

            return Ok(Contexto.ERROS);
        }

        [HttpPost("Aceitar")]
        public IActionResult Aceitar(OfertaEmprestimo oferta)
        {
            if (oferta == null)
                return BadRequest();

            aceitarOfertaService.Aceitar(oferta.OFERTAID);

            return Ok(Contexto.ERROS);
        }
    }
}
