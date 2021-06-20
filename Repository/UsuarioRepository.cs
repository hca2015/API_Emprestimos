using API_Emprestimos.Models;

namespace API_Emprestimos.Repository
{
    public class UsuarioRepository : AbstractRepository<Usuario>
    {
        public UsuarioRepository(BaseDbContext context) : base(context)
        {
        }

    }

}
