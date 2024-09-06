using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpMVC.Models
{
    public class PessoasModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Irá permitir o usuário inserir o número que quiser de ID
        public int ID { get; set; }

        public string Nome { get; set; }

        [StringLength(14)]
        public string CPF { get; set; }

        [StringLength(1)]
        public string Situacao { get; set; }

        public DateTime DataDeAlteracao { get; set; }
    }
}