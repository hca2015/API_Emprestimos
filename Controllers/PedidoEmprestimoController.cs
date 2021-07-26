using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using API_Emprestimos.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;

namespace API_Emprestimos.Controllers
{
    public class PedidoEmprestimoController : EntityController<PedidoEmprestimo, PedidoEmprestimoRepository>
    {
        private readonly CancelarPedidoService cancelarPedidoService;

        public PedidoEmprestimoController(IConfiguration configuration, IServiceProvider serviceProvider,
            ContextoExecucao contexto
            , PedidoEmprestimoRepository repository
            , CancelarPedidoService cancelarPedidoService)
            : base(configuration, serviceProvider, repository, contexto)
        {
            this.cancelarPedidoService = cancelarPedidoService;
        }

        [HttpGet]
        public List<PedidoEmprestimo> Get()
        {
            return repository.Get();
        }

        [HttpGet("Usuario")]
        public List<PedidoEmprestimo> GetUsuario()
        {
            return repository.GetUsuario();
        }

        [HttpPost("Cancelar")]
        public IActionResult Cancelar([FromQuery] int pedidoid)
        {

            if (cancelarPedidoService.Cancelar(pedidoid))
                return Ok();
            else
                return Problem(detail: Contexto.ERROS, statusCode: (int)HttpStatusCode.BadRequest);

        }
    }
}
