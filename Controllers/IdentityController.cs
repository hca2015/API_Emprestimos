using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace API_Emprestimos.Controllers
{
    public class IdentityController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UsuarioRepository usuarioRepository;

        public IdentityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IServiceProvider serviceProvider, UsuarioRepository usuarioRepository)
            : base(configuration, serviceProvider, null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.usuarioRepository = usuarioRepository;
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] Usuario model)
        {
            Debug.WriteLine("\n\n\n\n\nIncluindo usuario");

            using (TransactionScope scope = new(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled))
            {
                IdentityUser user = new() { UserName = model.EMAIL, Email = model.EMAIL };
                IdentityResult result = await _userManager.CreateAsync(user, model.PASSWORD);

                if (result.Succeeded)
                {
                    if (usuarioRepository.Find(model.EMAIL) == null && !usuarioRepository.Insert(model))
                    {
                        scope.Dispose();
                        throw new Exception("não foi possivel criar o usuario");
                    }

                    scope.Complete();
                    return BuildToken(model);
                }
                else
                {
                    scope.Dispose();
                    return BadRequest(string.Join("<br>", result.Errors.Select(e => e.Description)));
                }
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(userInfo.EMAIL, userInfo.PASSWORD,
                 isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return BuildToken(new Usuario() { EMAIL = userInfo.EMAIL });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "login inválido.");
                return BadRequest(ModelState);
            }
        }

        private UserToken BuildToken(Usuario userInfo)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.EMAIL),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            // tempo de expiração do token: 1 hora
            DateTime expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}