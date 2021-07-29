using API_Emprestimos.Repository;
using System;
using System.Diagnostics;

namespace API_Emprestimos.Service
{
    public class BaseService
    {
        private readonly ContextoExecucao contexto;
        [DebuggerStepThrough]
        public BaseService(ContextoExecucao contexto)
        {
            this.contexto = contexto;
        }

        [DebuggerStepThrough]
        protected virtual bool PreCondicao()
        {
            return true;
        }
        [DebuggerStepThrough]
        protected virtual bool Semantica()
        {
            return true;
        }

        [DebuggerStepThrough]
        private bool Action()
        {
            try
            {
                if (PreCondicao())
                    Semantica();
            }
            catch (Exception ex)
            {
                contexto.LISTAERROS.Add(ex.Message);
                return false;
            }

            return true;
        }
        [DebuggerStepThrough]
        public bool Executar()
        {
            return Action();
        }
    }
}
