using CSharpMVC.Models;
using CSharpMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CSharpMVC.Controllers
{
    public class TicketsController : Controller
    {
        // Pagina Index
        public IActionResult Index()
        {
            return View();
        }


        // Criar Ticket
        public IActionResult Criar()
        {
            return View();
        }


        // Gravar Ticket
        /*public IActionResult Criar(Model)
        {
            return View();
        }*/


        // Editar Pessoa
        public IActionResult Editar(int ID)
        {
            return View();
        }
    }
}