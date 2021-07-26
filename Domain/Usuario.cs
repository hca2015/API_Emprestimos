using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Emprestimos.Models
{
    public class Usuario : AbstractModel
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int USUARIOID { get; set; }

        [Required]
        [MaxLength(120)]
        public string NOME { get; set; }

        [Required]
        [MaxLength(120)]
        public string EMAIL { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "Não pode exceder 11 caracteres")]
        public string CPF { get; set; }

        [MaxLength(250, ErrorMessage = "Não pode exceder 250 caracteres")]
        public string ENDERECO { get; set; }

        [MaxLength(60, ErrorMessage = "Não pode exceder 60 caracteres")]
        public string BAIRRO { get; set; }

        [MaxLength(8, ErrorMessage = "Não pode exceder 8 caracteres")]
        public string CEP { get; set; }

        [MaxLength(60, ErrorMessage = "Não pode exceder 60 caracteres")]
        public string CIDADE { get; set; }

        [MaxLength(2, ErrorMessage = "Não pode exceder 2 caracteres")]
        public string ESTADO { get; set; }

        [NotMapped]
        public string PASSWORD { get; set; }
    }
}
