using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;

namespace API_Emprestimos.Controllers
{
    [Authorize]
    public class BaseAuthorizedController : BaseController
    {
        public BaseAuthorizedController(IConfiguration configuration, IServiceProvider serviceProvider) 
            : base (configuration, serviceProvider)
        {
        }
    }
}
