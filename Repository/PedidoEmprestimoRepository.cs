using API_Emprestimos.Models;
using System;

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
    }
}
