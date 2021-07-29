using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using System;

namespace API_Emprestimos.Service
{
    public class CancelarOfertaService : BaseService
    {
        private int Ofertaid;
        private readonly OfertaEmprestimoRepository OfertaEmprestimoRepository;
        private readonly AceiteEmprestimoRepository aceiteEmprestimoRepository;
        private readonly OfertaEmprestimoRepository ofertaEmprestimoRepository;

        public CancelarOfertaService(ContextoExecucao contexto
            , OfertaEmprestimoRepository OfertaEmprestimoRepository
            , AceiteEmprestimoRepository aceiteEmprestimoRepository
            , OfertaEmprestimoRepository ofertaEmprestimoRepository
            ) : base(contexto)
        {
            this.OfertaEmprestimoRepository = OfertaEmprestimoRepository;
            this.aceiteEmprestimoRepository = aceiteEmprestimoRepository;
            this.ofertaEmprestimoRepository = ofertaEmprestimoRepository;
        }

        protected override bool PreCondicao()
        {
            AceiteEmprestimo aceite = aceiteEmprestimoRepository.GetOferta(Ofertaid);

            if (aceite != null)
                throw new Exception("Não é possível cancelar uma Oferta que já foi aceita.");

            return base.PreCondicao();
        }

        protected override bool Semantica()
        {
            if (!OfertaEmprestimoRepository.Delete(Ofertaid))
                throw new Exception("Erro ao deletar");

            return base.Semantica();
        }

        public bool Cancelar(int Ofertaid)
        {
            this.Ofertaid = Ofertaid;

            return Executar();
        }
    }
}
