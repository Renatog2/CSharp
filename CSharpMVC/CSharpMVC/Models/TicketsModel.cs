using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSharpMVC.Models
{
    public class TicketsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Irá permitir o usuário inserir o número que quiser de ID
        public int ID { get; set; }

        public int FK_IDPessoas { get; set; }

        public int Quantidade { get; set; }

        [StringLength(1)]
        public string Situacao { get; set; }

        public DateTime DataDeEntrega { get; set; }
    }
}
