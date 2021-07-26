using API_Emprestimos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;

namespace API_Emprestimos.Controllers
{
    [Authorize]
    public class BaseAuthorizedController : BaseController
    {
        public BaseAuthorizedController(IConfiguration configuration, IServiceProvider serviceProvider, ContextoExecucao contexto)
            : base(configuration, serviceProvider, contexto)
        {
        }
    }
}
