using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using System;

namespace API_Emprestimos.Service
{
    public class AceitarOfertaService : BaseService
    {
        private int Ofertaid;
        private readonly OfertaEmprestimoRepository OfertaEmprestimoRepository;
        private readonly AceiteEmprestimoRepository aceiteEmprestimoRepository;
        private readonly OfertaEmprestimoRepository ofertaEmprestimoRepository;

        public AceitarOfertaService(ContextoExecucao contexto
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
                throw new Exception("Não é possível aceitar uma Oferta que já foi aceita.");

            return base.PreCondicao();
        }

        protected override bool Semantica()
        {
            OfertaEmprestimo ofertaEmprestimo = ofertaEmprestimoRepository.Find(Ofertaid);

            if (ofertaEmprestimo == null)
                throw new ArgumentNullException(nameof(ofertaEmprestimo));

            AceiteEmprestimo aceiteEmprestimo = new()
            {
                OFERTA = ofertaEmprestimo,
                PEDIDO = ofertaEmprestimo.PEDIDO,
                TAXAFINAL = ofertaEmprestimo.TAXA,
                TEMPOFINAL = ofertaEmprestimo.TEMPO,
                TIPOTEMPOFINAL = ofertaEmprestimo.TIPOTEMPO,
                VALORINICIAL = ofertaEmprestimo.PEDIDO.VALOR,
                VALORFINAL = ofertaEmprestimo.PEDIDO.VALOR + (ofertaEmprestimo.PEDIDO.VALOR * (ofertaEmprestimo.TAXA / 100) * ofertaEmprestimo.TEMPO),
                CREDOR = ofertaEmprestimo.USUARIO,
                REQUERENTE = ofertaEmprestimo.PEDIDO.USUARIO
            };

            if (!aceiteEmprestimoRepository.Insert(aceiteEmprestimo))
                throw new Exception("Erro ao inserir aceite");

            return base.Semantica();
        }

        public bool Aceitar(int Ofertaid)
        {
            this.Ofertaid = Ofertaid;

            return Executar();
        }
    }
}
