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

        internal List<AceiteEmprestimo> GetAll()
        {
            var retorno = Entity
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
                    
                .AsNoTracking()
                
                .OrderByDescending(x => x.ACEITO);

            return retorno.ToList();
        }
    }
}
