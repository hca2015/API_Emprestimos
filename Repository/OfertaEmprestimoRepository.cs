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

            if (abstractModel.USUARIO == null)
                abstractModel.USUARIO = usuarioRepository.Find(Contexto.USUARIOLOGIN);

            abstractModel.PEDIDO = pedidoEmprestimoRepository.Find(Convert.ToInt32(abstractModel.PEDIDO == null ? abstractModel.PEDIDOID : abstractModel.PEDIDO.PEDIDOID));
        }

        internal List<OfertaEmprestimo> GetUsuario()
        {
            IQueryable<OfertaEmprestimo> retorno = GetQueryable();

            retorno = retorno.Where(x => x.USUARIO.EMAIL == Contexto.USUARIOLOGIN && x.CANCELADO == 0 && x.ACEITO == null).OrderByDescending(x => x.CRIADO);

            return retorno.ToList();
        }

        internal OfertaEmprestimo Find(int ofertaid)
        {
            return GetQueryable().Where(x => x.OFERTAID == ofertaid).FirstOrDefault();
        }

        private IQueryable<OfertaEmprestimo> GetQueryable()
        {
            return Entity
                .Include(p => p.USUARIO)
                .Include(p => p.PEDIDO)

                .OrderByDescending(x => x.CRIADO);
        }

        internal List<OfertaEmprestimo> GetAll()
        {
            IQueryable<OfertaEmprestimo> retorno = GetQueryable();

            return retorno.ToList();
        }
    }

}
