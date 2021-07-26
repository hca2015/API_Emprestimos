using API_Emprestimos.Models;
using API_Emprestimos.Repository;
using System;

namespace API_Emprestimos.Service
{
    public class CancelarPedidoService : BaseService
    {
        private int pedidoid;
        private readonly PedidoEmprestimoRepository pedidoEmprestimoRepository;
        private readonly AceiteEmprestimoRepository aceiteEmprestimoRepository;
        private readonly OfertaEmprestimoRepository ofertaEmprestimoRepository;

        public CancelarPedidoService(ContextoExecucao contexto
            , PedidoEmprestimoRepository pedidoEmprestimoRepository
            , AceiteEmprestimoRepository aceiteEmprestimoRepository
            , OfertaEmprestimoRepository ofertaEmprestimoRepository
            ) : base(contexto)
        {
            this.pedidoEmprestimoRepository = pedidoEmprestimoRepository;
            this.aceiteEmprestimoRepository = aceiteEmprestimoRepository;
            this.ofertaEmprestimoRepository = ofertaEmprestimoRepository;
        }

        protected override bool PreCondicao()
        {
            AceiteEmprestimo aceite = aceiteEmprestimoRepository.GetPedido(pedidoid);

            if (aceite != null)
                throw new Exception("Não é possível cancelar um pedido que já foi aceito.");

            return base.PreCondicao();
        }

        protected override bool Semantica()
        {
            if (!pedidoEmprestimoRepository.Delete(pedidoid))
                throw new Exception("Erro ao deletar");

            return base.Semantica();
        }

        public bool Cancelar(int pedidoid)
        {
            this.pedidoid = pedidoid;

            return Executar();
        }
    }
}
