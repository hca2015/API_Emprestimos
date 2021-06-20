using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Emprestimos.Models
{
    public class AceiteEmprestimo : AbstractModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ACEITEID { get; set; }
        [Required]
        public virtual PedidoEmprestimo PEDIDO { get; set; }
        [Required]
        public virtual OfertaEmprestimo OFERTA { get; set; }
        [Required]
        public DateTime ACEITO { get; set; }
        [Required]
        public double TAXAFINAL { get; set; }
        [Required]
        public double VALORINICIAL { get; set; }
        [Required]
        public double VALORFINAL { get; set; }
        [Required]
        public double TEMPOFINAL { get; set; }
        [Required]
        public double TIPOTEMPOFINAL { get; set; } //KDTipoTempo
        [Required]
        public Usuario REQUERENTE { get; set; }
        [Required]
        public Usuario CREDOR { get; set; }
    }
}
