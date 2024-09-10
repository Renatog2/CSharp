using CSharpMVC.Models;
using CSharpMVC.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

public class RelatoriosController : Controller
{
    private readonly ITicketsRepository _ticketsRepository;
    private readonly IPessoasRepository _pessoasRepository;

    public RelatoriosController(ITicketsRepository ticketsRepository, IPessoasRepository pessoasRepository)
    {
        _ticketsRepository = ticketsRepository;
        _pessoasRepository = pessoasRepository;
    }

    public IActionResult Index()
    {
        var relatorioViewModel = new RelatorioViewModel
        {
            Pessoas = _pessoasRepository.BuscarTodos().Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Nome }).ToList(),
            Tickets = _ticketsRepository.BuscarTodos()
        };

        return View(relatorioViewModel);
    }


    // Obter os dados para Visualização
    [HttpPost]
    public IActionResult Relatorio(RelatorioViewModel filtro)
    {
        // Obter as pessoas para o dropdown
        filtro.Pessoas = _pessoasRepository.BuscarTodos().Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Nome }).ToList();
        var tickets = _ticketsRepository.BuscarTodos();

        // Filtros, por ser DATETIME garante que na pesquisa os segundos iniciais serão sempre zero e os finais 59
        if (filtro.DataInicio.HasValue)
        {
            filtro.DataInicio = filtro.DataInicio.Value.AddSeconds(-filtro.DataInicio.Value.Second);
            tickets = tickets.Where(t => t.DataDeEntrega >= filtro.DataInicio.Value).ToList();
        }
        if (filtro.DataFim.HasValue)
        {
            filtro.DataFim = filtro.DataFim.Value.AddSeconds(59 - filtro.DataFim.Value.Second);
            tickets = tickets.Where(t => t.DataDeEntrega <= filtro.DataFim.Value).ToList();
        }
        if (filtro.PessoaId.HasValue)
        {
            tickets = tickets.Where(t => t.FK_IDPessoas == filtro.PessoaId.Value).ToList();
        }
        filtro.Tickets = tickets;

        // Totalizador de Tickets
        ViewBag.TotalQuantidade = tickets.Sum(t => t.Quantidade);

        return View("Index", filtro);
    }
}
