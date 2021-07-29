using API_Emprestimos.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Emprestimos.Repository
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasKey(t => t.USUARIOID);
            modelBuilder.Entity<Usuario>().HasIndex(t => t.CPF).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(t => t.EMAIL).IsUnique();


            modelBuilder.Entity<PedidoEmprestimo>().HasKey(t => t.PEDIDOID);
            modelBuilder.Entity<PedidoEmprestimo>().HasOne(t => t.USUARIO);
            modelBuilder.Entity<PedidoEmprestimo>().HasMany(t => t.Ofertas)
                .WithOne(t => t.PEDIDO)
                .HasForeignKey(o => o.PEDIDOID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<OfertaEmprestimo>().HasKey(t => t.OFERTAID);
            modelBuilder.Entity<OfertaEmprestimo>().HasOne(t => t.USUARIO);
            modelBuilder.Entity<OfertaEmprestimo>().Property(t => t.CANCELADO).HasDefaultValue(0);


            modelBuilder.Entity<AceiteEmprestimo>().HasKey(t => t.ACEITEID);
            modelBuilder.Entity<AceiteEmprestimo>().HasOne(t => t.PEDIDO);
            modelBuilder.Entity<AceiteEmprestimo>().HasOne(t => t.OFERTA);
        }
    }
}
