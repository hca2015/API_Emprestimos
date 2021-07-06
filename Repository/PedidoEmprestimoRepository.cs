using API_Emprestimos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Emprestimos.Repository
{
    public class PedidoEmprestimoRepository : AbstractRepository<PedidoEmprestimo>
    {
        public PedidoEmprestimoRepository(BaseDbContext context) : base(context)
        {
        }

        protected override void BeforeInsert(PedidoEmprestimo abstractModel)
        {
            abstractModel.CRIADO = DateTime.Now;
        }

        internal List<PedidoEmprestimo> GetAll()
        {
            var retorno = Entity
                .Include(p => p.USUARIO)
                .Include(p => p.Ofertas)
                    .ThenInclude(u => u.USUARIO)
                .Include(p => p.Ofertas)
                    .ThenInclude(p => p.PEDIDO)

                .OrderByDescending(x => x.CRIADO);

            return retorno.ToList();
        }
    }
}
