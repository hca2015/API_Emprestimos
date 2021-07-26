using API_Emprestimos.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;

namespace API_Emprestimos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class BaseController : Controller
    {
        protected readonly IConfiguration configuration;
        protected readonly IServiceProvider serviceProvider;
        protected readonly ContextoExecucao Contexto;

        public BaseController(IConfiguration configuration, IServiceProvider serviceProvider, ContextoExecucao contexto)
        {
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
            Contexto = contexto;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ClaimsPrincipal lTokenUser = context.HttpContext.User;

            if (lTokenUser != null && Contexto != null)
            {
                string lLogin = lTokenUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

                if (lLogin != null)
                    Contexto.USUARIOLOGIN = lLogin;
            }
        }
    }
}
