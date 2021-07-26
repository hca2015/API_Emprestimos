using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Emprestimos.Models
{
    public class PedidoEmprestimo : AbstractModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PEDIDOID { get; set; }
        //[Required]
        public virtual Usuario USUARIO { get; set; }
        [Required]
        public double VALOR { get; set; }
        [Required]
        public DateTime CRIADO { get; set; }

        public List<OfertaEmprestimo> Ofertas { get; set; } = new List<OfertaEmprestimo>();

        public DateTime? ACEITO { get; set; }
        [NotMapped]
        public string CRIADOSTR => CRIADO.ToString("d");
        [NotMapped]
        public string ACEITOSTR => ACEITO?.ToString("d");
    }
}
