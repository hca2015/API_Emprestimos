using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API_Emprestimos.Controllers
{
    public class TesteController : BaseController
    {
        public TesteController(IConfiguration configuration,
            IServiceProvider serviceProvider,
            UsuarioRepository usuarioRepository,
            PedidoEmprestimoRepository pedidoEmprestimoRepository,
            OfertaEmprestimoRepository ofertaEmprestimoRepository,
            AceiteEmprestimoRepository aceiteEmprestimoRepository)
            : base(configuration, serviceProvider, null)
        {
            this.usuarioRepository = usuarioRepository;
            this.pedidoEmprestimoRepository = pedidoEmprestimoRepository;
            this.ofertaEmprestimoRepository = ofertaEmprestimoRepository;
            this.aceiteEmprestimoRepository = aceiteEmprestimoRepository;
        }

        private readonly List<Usuario> usuarios = new()
        {
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO = "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78965412308",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "haryel",
                EMAIL = "haryel@mail.com",
                PASSWORD = "123456789Zx!"
            },
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO = "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78967812308",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "gabriel",
                EMAIL = "gabriel@mail.com",
                PASSWORD = "123456789Zx!"
            },
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO = "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78967412308",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "beatriz",
                EMAIL = "beatriz@mail.com",
                PASSWORD = "123456789Zx!"
            },
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO = "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78965892308",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "wesley",
                EMAIL = "wesley@mail.com",
                PASSWORD = "123456789Zx!"
            },
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO = "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78965412708",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "everson",
                EMAIL = "everson@mail.com",
                PASSWORD = "123456789Zx!"
            },
        };

        private readonly List<PedidoEmprestimo> pedidos = new();

        private readonly List<OfertaEmprestimo> ofertas = new();

        private readonly UsuarioRepository usuarioRepository;
        private readonly PedidoEmprestimoRepository pedidoEmprestimoRepository;
        private readonly OfertaEmprestimoRepository ofertaEmprestimoRepository;
        private readonly AceiteEmprestimoRepository aceiteEmprestimoRepository;

        [HttpPost]
        public void Testar()
        {
            Debugger.Break();
            bool soInserir = false;

            IdentityContext context = serviceProvider.GetService(typeof(IdentityContext)) as IdentityContext;
            List<Microsoft.AspNetCore.Identity.IdentityUser> users = context.Users.ToList();
            if (users.Count > 0)
                soInserir = true;

            aceiteEmprestimoRepository.WipeData();
            ofertaEmprestimoRepository.WipeData();
            pedidoEmprestimoRepository.WipeData();
            usuarioRepository.WipeData();

            List<Task> tasks = new();

            foreach (Usuario item in usuarios)
            {
                if (soInserir)
                {
                    if (!usuarioRepository.Insert(item))
                        Debugger.Break();
                }
                else
                {
                    Task t = new(
                        async () =>
                        {
                            try
                            {
                                HttpClient http = new();
                                HttpContent content = JsonContent.Create(item);
                                await http.PostAsync("https://localhost:5001/api/Identity/Criar", content);
                            }
                            catch (Exception e)
                            {
                                Debugger.Break();
                            }
                        });
                    t.Start();
                    tasks.Add(t);
                }
            }

            Task.WaitAll(tasks.ToArray());

            for (int i = 0; i < 3; i++)
            {
                foreach (Usuario item in usuarios)
                {
                    PedidoEmprestimo pedido = new()
                    {
                        USUARIO = item,
                        VALOR = new Random().Next(12335, 78411) + Math.Round(new Random().NextDouble(), 2)
                    };

                    if (pedidoEmprestimoRepository.Insert(pedido))
                        pedidos.Add(pedido);
                    else
                        Debugger.Break();
                }
            }

            foreach (Usuario usu in usuarios)
            {
                foreach (PedidoEmprestimo ped in pedidos)
                {
                    OfertaEmprestimo oferta = new()
                    {
                        PEDIDO = ped,
                        USUARIO = usu,
                        TAXA = Math.Round(new Random().NextDouble(), 5),
                        TEMPO = new Random().Next(30, 90),
                        TIPOTEMPO = (int)KDTipoTempo.Dias
                    };

                    if (ofertaEmprestimoRepository.Insert(oferta))
                        ofertas.Add(oferta);
                    else
                        Debugger.Break();
                }
            }

            //esta assim pq o teste so gera dois pedidos
            List<OfertaEmprestimo> ofertas1 = ofertas.Where(x => x.PEDIDO == pedidos.First()).ToList();
            List<OfertaEmprestimo> ofertas2 = ofertas.Where(x => x.PEDIDO == pedidos.Last()).ToList();

            List<OfertaEmprestimo> ofertasAceitas = new()
            {
                ofertas1.First(),
                ofertas2.First()
            };

            foreach (OfertaEmprestimo item in ofertasAceitas)
            {
                AceiteEmprestimo aceite = new AceiteEmprestimo()
                {
                    OFERTA = item,
                    PEDIDO = item.PEDIDO,
                    TAXAFINAL = item.TAXA,
                    TEMPOFINAL = item.TEMPO,
                    TIPOTEMPOFINAL = item.TIPOTEMPO,
                    VALORINICIAL = item.PEDIDO.VALOR,
                    VALORFINAL = item.PEDIDO.VALOR + (item.PEDIDO.VALOR * (item.TAXA / 100) * item.TEMPO),
                    CREDOR = item.USUARIO,
                    REQUERENTE = item.PEDIDO.USUARIO
                };

                if (!aceiteEmprestimoRepository.Insert(aceite))
                    Debugger.Break();
            }

            Debugger.Break();
        }
    }
}
