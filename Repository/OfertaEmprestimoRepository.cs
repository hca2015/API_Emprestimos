using API_Emprestimos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Emprestimos.Repository
{
    public class OfertaEmprestimoRepository : AbstractRepository<OfertaEmprestimo>
    {
        public OfertaEmprestimoRepository(BaseDbContext context) : base(context)
        {
        }

        protected override void BeforeInsert(OfertaEmprestimo abstractModel)
        {
            abstractModel.CRIADO = DateTime.Now;
        }

        internal List<OfertaEmprestimo> GetAll()
        {
            var retorno = Entity
                .Include(p => p.USUARIO)
                .Include(p => p.PEDIDO)

                .AsNoTracking()

                .OrderByDescending(x => x.CRIADO);

            return retorno.ToList();
        }
    }

}
