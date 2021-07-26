using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Emprestimos.Models
{
    public class OfertaEmprestimo : AbstractModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OFERTAID { get; set; }
        public virtual Usuario USUARIO { get; set; }
        public virtual PedidoEmprestimo PEDIDO { get; set; }
        [Required]
        public double TAXA { get; set; } //0.25..etc
        [Required]
        public int TEMPO { get; set; } //Quantidade de dias, meses, anos
        [Required]
        public int TIPOTEMPO { get; set; } //KDTipoTempo
        public DateTime CRIADO { get; set; }
        public int CANCELADO { get; set; }
        public int PEDIDOID { get; set; }
    }
}
