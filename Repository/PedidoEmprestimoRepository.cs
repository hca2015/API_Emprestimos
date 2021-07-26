using API_Emprestimos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Emprestimos.Repository
{
    public class PedidoEmprestimoRepository : AbstractRepository<PedidoEmprestimo>
    {
        private readonly UsuarioRepository usuarioRepository;

        public PedidoEmprestimoRepository(BaseDbContext context
            , ContextoExecucao contexto
            , UsuarioRepository usuarioRepository) : base(context, contexto)
        {
            this.usuarioRepository = usuarioRepository;
        }

        protected override void BeforeInsert(PedidoEmprestimo abstractModel)
        {
            abstractModel.CRIADO = DateTime.Now;

            abstractModel.USUARIO = usuarioRepository.Find(Contexto.USUARIOLOGIN);
        }

        private IQueryable<PedidoEmprestimo> GetQueryable()
        {
            return Entity
                .Include(p => p.USUARIO)
                .Include(p => p.Ofertas)
                    .ThenInclude(u => u.USUARIO)
                .Include(p => p.Ofertas)
                    .ThenInclude(p => p.PEDIDO)

                .OrderByDescending(x => x.CRIADO);
        }

        internal PedidoEmprestimo Find(int pedidoid)
        {
            return GetQueryable().Where(x => x.PEDIDOID == pedidoid).FirstOrDefault();
        }

        internal List<PedidoEmprestimo> GetAll()
        {
            IQueryable<PedidoEmprestimo> retorno = GetQueryable();

            return retorno.ToList();
        }

        internal List<PedidoEmprestimo> GetUsuario()
        {
            IQueryable<PedidoEmprestimo> retorno = GetQueryable();

            retorno = retorno.Where(x => x.USUARIO.EMAIL == Contexto.USUARIOLOGIN).OrderByDescending(x => x.CRIADO);

            return retorno.ToList();
        }

        internal List<PedidoEmprestimo> Get()
        {
            IQueryable<PedidoEmprestimo> retorno = GetQueryable();

            retorno = retorno.Where(x => x.USUARIO.EMAIL != Contexto.USUARIOLOGIN && x.ACEITO == null).OrderByDescending(x => x.USUARIO.NOME);

            return retorno.ToList();
        }
    }
}
