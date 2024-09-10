using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSharpMVC.Models
{
    public class RelatorioViewModel
    {
        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public int? PessoaId { get; set; }

        public string Situacao { get; set; }

        public List<SelectListItem> Pessoas { get; set; }

        public List<TicketsModel> Tickets { get; set; }
    }
}
