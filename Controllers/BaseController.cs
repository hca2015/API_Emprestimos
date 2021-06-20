using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace API_Emprestimos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class BaseController : ControllerBase
    {
        protected readonly IConfiguration configuration;
        protected readonly IServiceProvider serviceProvider;

        public BaseController(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
        }
    }
}
