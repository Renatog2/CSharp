using CSharpMVC.Models;
using CSharpMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CSharpMVC.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IPessoasRepository _pessoasRepository;

        public TicketsController(ITicketsRepository ticketsRepository, IPessoasRepository pessoasRepository)
        {
            _ticketsRepository = ticketsRepository;
            _pessoasRepository = pessoasRepository;
        }


        // Pagina Index
        public IActionResult Index()
        {
            List<TicketsModel> tickets = _ticketsRepository.BuscarTodos();

            // Obter o nome da pessoa para listagem na Index
            Dictionary<int, string> pessoasNomes = new Dictionary<int, string>();
            foreach (var ticket in tickets)
            {
                var pessoa = _pessoasRepository.BuscarPessoa(ticket.FK_IDPessoas);
                if (pessoa != null)
                {
                    pessoasNomes[ticket.FK_IDPessoas] = pessoa.Nome;
                }
            }

            ViewBag.PessoasNomes = pessoasNomes;
            return View(tickets);
        }


        // Criar Ticket
        public IActionResult Criar()
        {
            // Obter o último ID cadastrado e passa para a View como sugestão
            int proximoId = _ticketsRepository.ObterUltimoID() + 1;
            ViewBag.ProximoId = proximoId;

            // Buscar as Pessoas para o cadastro de Ticket
            var pessoas = _pessoasRepository.BuscarTodos();
            ViewBag.Pessoas = pessoas;

            return View();
        }


        // Gravar Ticket
        [HttpPost]
        public IActionResult Criar(TicketsModel ticketsModel)
        {
            try
            {
                // Verifica se o ID já existe no banco de dados
                var ticketExistente = _ticketsRepository.BuscarTicket(ticketsModel.ID);
                if (ticketExistente != null)
                {
                    ModelState.AddModelError("ID", "Já existe um Ticket cadastrado com esse ID.");
                }

                // Impede cadastro de novos Tickets com a situação Inativo
                if (ticketsModel.Situacao == "I")
                {
                    ModelState.AddModelError("Situacao", "Não é permitido cadastrar um Ticket com situação Inativo");
                }

                // Para quando nenhuma validação apresentou erros
                if (ModelState.IsValid)
                {
                    _ticketsRepository.Adicionar(ticketsModel);
                    TempData["Sucesso"] = "Operação realizada com sucesso";
                    return RedirectToAction("Index");
                }

                // Se houver erros de validação, retorna a mesma view com o modelo e os erros
                int proximoId = _ticketsRepository.ObterUltimoID() + 1;
                ViewBag.ProximoId = proximoId;
                var pessoas = _pessoasRepository.BuscarTodos();
                ViewBag.Pessoas = pessoas;
                return View(ticketsModel);

            }
            catch (Exception ex)
            {
                TempData["Falha"] = $"Ocorreu um erro ao realizar a Operação: + {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        // Editar Ticket
        public IActionResult Editar(int id)
        {
            var ticket = _ticketsRepository.BuscarTicket(id);

            // Retornar as pessoas do Ticket
            var pessoas = _pessoasRepository.BuscarTodos();
            ViewBag.Pessoas = pessoas;

            return View(ticket);
        }
        [HttpPost]
        public IActionResult Atualizar(TicketsModel ticketsModel)
        {
            try
            {
                // Para quando nenhuma validação apresentou erros
                if (ModelState.IsValid)
                {
                    _ticketsRepository.Atualizar(ticketsModel);
                    TempData["Sucesso"] = "Operação realizada com sucesso";
                    return RedirectToAction("Index");
                }

                // Se houver erros de validação, retorna a view com o modelo e os erros
                return View("Editar", ticketsModel);
            }
            catch (Exception ex)
            {
                TempData["Falha"] = $"Ocorreu um erro ao realizar a Operação:  + {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}