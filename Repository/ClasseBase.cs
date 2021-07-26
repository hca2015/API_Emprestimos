using System.Collections.Generic;

namespace API_Emprestimos.Repository
{
    public class ClasseBase
    {
        protected ContextoExecucao Contexto { get; set; }

        public ClasseBase(ContextoExecucao Contexto)
        {
            this.Contexto = Contexto;
        }
    }

    public class ContextoExecucao
    {

        public ContextoExecucao()
        {
            LISTAERROS = new List<string>();
        }

        public string USUARIOLOGIN { get; set; }
        public string ERROS { get => string.Join(", ", LISTAERROS); }
        public List<string> LISTAERROS { get; set; }

    }
}