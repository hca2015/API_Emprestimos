using API_Emprestimos.Models;
using System;

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
    }

}
