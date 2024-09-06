using CSharpMVC.Models;
using CSharpMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CSharpMVC.Controllers
{
    public class PessoasController : Controller
    {
        private readonly IPessoasRepository _pessoasRepository;

        public PessoasController(IPessoasRepository pessoasRepository)
        {
            _pessoasRepository = pessoasRepository;
        }


        // Pagina Index com valor do ID sugerido
        public IActionResult Index()
        {
            List<PessoasModel> pessoas = _pessoasRepository.BuscarTodos();
            return View(pessoas);
        }


        // Criar Pessoa
        public IActionResult Criar()
        {
            // Obter o último ID cadastrado e passa para a View como sugestão
            int proximoId = _pessoasRepository.ObterUltimoID() + 1;
            ViewBag.ProximoId = proximoId;
            return View();
        }


        // Gravar Pessoa
        [HttpPost]
        public IActionResult Criar(PessoasModel pessoasModel)
        {
            try
            {
                // Verifica se o ID já existe no banco de dados
                var pessoaExistente = _pessoasRepository.BuscarPessoa(pessoasModel.ID);
                if (pessoaExistente != null)
                {
                    ModelState.AddModelError("ID", "Já existe uma pessoa cadastrada com esse ID.");
                }

                // Impede cadastro de novas Pessoas com a situação Inativo
                if (pessoasModel.Situacao == "I")
                {
                    ModelState.AddModelError("Situacao", "Não é permitido cadastrar uma pessoa com situação Inativo");
                }

                // Impede cadastro de novas Pessoas com o CPF Duplicado
                var CPFExistente = _pessoasRepository.BuscarCPF(pessoasModel.CPF);
                if (CPFExistente != null)
                {
                    ModelState.AddModelError("CPF", "Já existe uma pessoa cadastrada com esse CPF.");
                }

                // Para quando nenhuma validação apresentou erros
                if (ModelState.IsValid)
                {
                    _pessoasRepository.Adicionar(pessoasModel);
                    TempData["Sucesso"] = "Operação realizada com sucesso";
                    return RedirectToAction("Index");
                }

                // Se houver erros de validação, retorna a mesma view com o modelo e os erros
                return View(pessoasModel);
            }
            catch (Exception ex)
            {
                TempData["Falha"] = $"Ocorreu um erro ao realizar a Operação:  + {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        // Editar Pessoa
        public IActionResult Editar(int ID)
        {
            PessoasModel pessoasModel = _pessoasRepository.BuscarPessoa(ID);
            return View(pessoasModel);
        }
        [HttpPost]
        public IActionResult Atualizar(PessoasModel pessoasModel)
        {
            try
            {
                // Impede duplicar o CPF com alguém já cadatrado
                var CPFExistente = _pessoasRepository.BuscarCPF(pessoasModel.CPF);
                if (CPFExistente != null && CPFExistente.ID != pessoasModel.ID)
                    if (CPFExistente != null)
                {
                    ModelState.AddModelError("CPF", "Já existe uma pessoa cadastrada com esse CPF.");
                }

                // Para quando nenhuma validação apresentou erros
                if (ModelState.IsValid)
                {
                    _pessoasRepository.Atualizar(pessoasModel);
                    TempData["Sucesso"] = "Operação realizada com sucesso";
                    return RedirectToAction("Index");
                }

                // Se houver erros de validação, retorna a view com o modelo e os erros
                return View("Editar", pessoasModel);
            }
            catch (Exception ex)
            {
                TempData["Falha"] = $"Ocorreu um erro ao realizar a Operação:  + {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
