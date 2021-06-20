using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace API_Emprestimos.Controllers
{
    public abstract class EntityController<Entity, Repository> : BaseAuthorizedController
        where Entity : AbstractModel
        where Repository : AbstractRepository<Entity>
    {
        private protected Repository repository;

        protected EntityController(IConfiguration configuration, IServiceProvider serviceProvider, Repository repository)
            : base(configuration, serviceProvider)
        {
            this.repository = repository;
        }

        [HttpPost("Criar")]
        public IActionResult Criar(Entity model)
        {
            repository.Insert(model);

            return Ok(model);
        }

        [HttpPut("Atualizar")]
        public IActionResult Atualizar(Entity model)
        {
            repository.Update(model);

            return Ok(model);
        }

        [HttpPost("Deletar")]
        public IActionResult Deletar(Entity model)
        {
            repository.Delete(model);

            return Ok();
        }
    }
}
