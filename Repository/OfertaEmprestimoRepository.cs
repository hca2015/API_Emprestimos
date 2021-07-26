using API_Emprestimos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Emprestimos.Repository
{
    public class OfertaEmprestimoRepository : AbstractRepository<OfertaEmprestimo>
    {
        private readonly UsuarioRepository usuarioRepository;
        private readonly PedidoEmprestimoRepository pedidoEmprestimoRepository;

        public OfertaEmprestimoRepository(BaseDbContext context, ContextoExecucao contexto, UsuarioRepository usuarioRepository, PedidoEmprestimoRepository pedidoEmprestimoRepository)
            : base(context, contexto)
        {
            this.usuarioRepository = usuarioRepository;
            this.pedidoEmprestimoRepository = pedidoEmprestimoRepository;
        }

        protected override void BeforeInsert(OfertaEmprestimo abstractModel)
        {
            abstractModel.CRIADO = DateTime.Now;
            abstractModel.USUARIO = usuarioRepository.Find(Contexto.USUARIOLOGIN);
            abstractModel.PEDIDO = pedidoEmprestimoRepository.Find(abstractModel.PEDIDOID);
        }

        internal List<OfertaEmprestimo> GetAll()
        {
            IOrderedQueryable<OfertaEmprestimo> retorno = Entity
                .Include(p => p.USUARIO)
                .Include(p => p.PEDIDO)

                .OrderByDescending(x => x.CRIADO);

            return retorno.ToList();
        }

        internal List<OfertaEmprestimo> GetPedido(int pedidoid)
        {
            throw new NotImplementedException();
        }
    }

}
