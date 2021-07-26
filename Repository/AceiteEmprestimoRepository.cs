using API_Emprestimos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Emprestimos.Repository
{
    public class AceiteEmprestimoRepository : AbstractRepository<AceiteEmprestimo>
    {
        private readonly OfertaEmprestimoRepository ofertaEmprestimoRepository;
        private readonly PedidoEmprestimoRepository pedidoEmprestimoRepository;

        public AceiteEmprestimoRepository(BaseDbContext context,
            OfertaEmprestimoRepository ofertaEmprestimoRepository,
            PedidoEmprestimoRepository pedidoEmprestimoRepository,
            ContextoExecucao contexto) : base(context, contexto)
        {
            this.ofertaEmprestimoRepository = ofertaEmprestimoRepository;
            this.pedidoEmprestimoRepository = pedidoEmprestimoRepository;
        }

        protected override void BeforeInsert(AceiteEmprestimo abstractModel)
        {
            abstractModel.ACEITO = DateTime.Now;

            List<OfertaEmprestimo> ofertas = abstractModel.PEDIDO.Ofertas;

            foreach (OfertaEmprestimo item in ofertas)
            {
                if (item == abstractModel.OFERTA)
                    continue;

                item.CANCELADO = 1;
                ofertaEmprestimoRepository.Update(item);
            }

            abstractModel.PEDIDO.ACEITO = DateTime.Now;
            pedidoEmprestimoRepository.Update(abstractModel.PEDIDO);
        }

        private IQueryable<AceiteEmprestimo> GetQueryable()
        {
            return Entity
                .Include(p => p.CREDOR)
                .Include(p => p.REQUERENTE)

                .Include(p => p.OFERTA)
                    .ThenInclude(u => u.USUARIO)
                .Include(p => p.OFERTA)
                    .ThenInclude(p => p.PEDIDO)

                .Include(p => p.PEDIDO)
                .ThenInclude(p => p.USUARIO)
                .Include(p => p.PEDIDO)
                .ThenInclude(p => p.Ofertas)
                    .ThenInclude(u => u.USUARIO)
                .Include(p => p.PEDIDO)
                    .ThenInclude(p => p.Ofertas)
                    .ThenInclude(p => p.PEDIDO)

                .OrderByDescending(x => x.ACEITO);
        }

        internal List<AceiteEmprestimo> GetAll()
        {
            IQueryable<AceiteEmprestimo> retorno = GetQueryable();

            return retorno.ToList();
        }

        internal AceiteEmprestimo GetPedido(int pedidoid)
        {
            IQueryable<AceiteEmprestimo> retorno = GetQueryable();

            retorno = retorno.Where(x => x.PEDIDO.PEDIDOID == pedidoid);

            return retorno.FirstOrDefault();
        }
    }
}
