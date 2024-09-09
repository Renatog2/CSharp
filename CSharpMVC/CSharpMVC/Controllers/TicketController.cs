using CSharpMVC.Models;
using CSharpMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CSharpMVC.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketsRepository _ticketsRepository;

        public TicketsController(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }


        // Pagina Index com valor do ID sugerido
        public IActionResult Index()
        {
            List<TicketsModel> tickets = _ticketsRepository.BuscarTodos();
            return View(tickets);
        }


        // Criar Ticket
        public IActionResult Criar()
        {
            // Obter o último ID cadastrado e passa para a View como sugestão
            int proximoId = _ticketsRepository.ObterUltimoID() + 1;
            ViewBag.ProximoId = proximoId;
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
                return View(ticketsModel);

            }
            catch (Exception ex)
            {
                TempData["Falha"] = $"Ocorreu um erro ao realizar a Operação:  + {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        // Editar Ticket
        public IActionResult Editar(int ID)
        {
            TicketsModel ticketsModel = _ticketsRepository.BuscarTicket(ID);
            return View(ticketsModel);
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