using API_Emprestimos.Models;
using System;

namespace API_Emprestimos.Repository
{
    public class AceiteEmprestimoRepository : AbstractRepository<AceiteEmprestimo>
    {
        private readonly OfertaEmprestimoRepository ofertaEmprestimoRepository;

        public AceiteEmprestimoRepository(BaseDbContext context, OfertaEmprestimoRepository ofertaEmprestimoRepository) : base(context)
        {
            this.ofertaEmprestimoRepository = ofertaEmprestimoRepository;
        }

        protected override void BeforeInsert(AceiteEmprestimo abstractModel)
        {
            abstractModel.ACEITO = DateTime.Now;

            System.Collections.Generic.List<OfertaEmprestimo> ofertas = abstractModel.PEDIDO.Ofertas;
            
            foreach (OfertaEmprestimo item in ofertas)
            {
                if (item == abstractModel.OFERTA)
                    continue;

                item.CANCELADO = 1;
                ofertaEmprestimoRepository.Update(item);
            }
        }
    }
}
