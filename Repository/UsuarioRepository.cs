using API_Emprestimos.Models;
using System.Collections.Generic;
using System.Linq;

namespace API_Emprestimos.Repository
{
    public class UsuarioRepository : AbstractRepository<Usuario>
    {
        public UsuarioRepository(BaseDbContext context, ContextoExecucao contexto) : base(context, contexto)
        {
        }

        public Usuario Find(string email)
        {
            if (email == null)
                return null;

            return Entity.FirstOrDefault(x => x.EMAIL.ToLower().Trim() == email.ToLower().Trim());
        }

        internal List<Usuario> GetAll()
        {
            return Entity.ToList();
        }
    }

}
