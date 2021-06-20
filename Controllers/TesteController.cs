using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            : base(configuration, serviceProvider)
        {
            this.usuarioRepository = usuarioRepository;
            this.pedidoEmprestimoRepository = pedidoEmprestimoRepository;
            this.ofertaEmprestimoRepository = ofertaEmprestimoRepository;
            this.aceiteEmprestimoRepository = aceiteEmprestimoRepository;
        }

        private List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO= "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78965412308",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "haryel",
                EMAIL = "haryel@mail.com"
            },
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO= "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78967812308",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "gabriel",
                EMAIL = "gabriel@mail.com"
            },
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO= "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78967412308",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "beatriz",
                EMAIL = "beatriz@mail.com"
            },
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO= "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78965892308",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "wesley",
                EMAIL = "wesley@mail.com"
            },
            new Usuario()
            {
                CEP = "11045400",
                BAIRRO= "MARAPE",
                CIDADE = "SANTOS",
                CPF = "78965412708",
                ENDERECO = "AVENIDA MARECHAL DEODORO, 18",
                ESTADO = "SP",
                NOME = "everson",
                EMAIL = "everson@mail.com"
            },
        };

        private List<PedidoEmprestimo> pedidos = new List<PedidoEmprestimo>();

        private List<OfertaEmprestimo> ofertas = new List<OfertaEmprestimo>();

        private readonly UsuarioRepository usuarioRepository;
        private readonly PedidoEmprestimoRepository pedidoEmprestimoRepository;
        private readonly OfertaEmprestimoRepository ofertaEmprestimoRepository;
        private readonly AceiteEmprestimoRepository aceiteEmprestimoRepository;

        [HttpPost]
        public void Testar()
        {
            Debugger.Break();

            aceiteEmprestimoRepository.WipeData();
            ofertaEmprestimoRepository.WipeData();
            pedidoEmprestimoRepository.WipeData();
            usuarioRepository.WipeData();
                        
            foreach (Usuario item in usuarios)
            {
                if (!usuarioRepository.Insert(item))
                    Debugger.Break();
            }

            foreach (Usuario item in usuarios.Take(2))
            {
                PedidoEmprestimo pedido = new PedidoEmprestimo()
                {
                    USUARIO = item,
                    VALOR = new Random().Next(12335, 78411)
                };

                if (pedidoEmprestimoRepository.Insert(pedido))
                    pedidos.Add(pedido);
                else
                    Debugger.Break();
            }

            foreach (Usuario usu in usuarios.Skip(2))
            {
                foreach (PedidoEmprestimo ped in pedidos)
                {
                    OfertaEmprestimo oferta = new OfertaEmprestimo()
                    {
                        PEDIDO = ped,
                        USUARIO = usu,
                        TAXA = new Random().NextDouble(),
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

            List<OfertaEmprestimo> ofertasAceitas = new List<OfertaEmprestimo>
            {
                ofertas1.First(),
                ofertas2.First()
            };

            foreach (var item in ofertasAceitas)
            {
                var aceite = new AceiteEmprestimo()
                {
                    OFERTA = item,
                    PEDIDO = item.PEDIDO,
                    TAXAFINAL = item.TAXA,
                    TEMPOFINAL = item.TEMPO,
                    TIPOTEMPOFINAL = item.TIPOTEMPO,
                    VALORINICIAL = item.PEDIDO.VALOR,
                    VALORFINAL = item.PEDIDO.VALOR + ( item.PEDIDO.VALOR * (item.TAXA / 100) * item.TEMPO ),
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
