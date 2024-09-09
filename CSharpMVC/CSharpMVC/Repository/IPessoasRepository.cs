using CSharpMVC.Models;

namespace CSharpMVC.Repository
{
    public interface IPessoasRepository
    {
        PessoasModel BuscarPessoa(int ID);

        IEnumerable<PessoasModel> ObterPessoasPaginadas(int pageNumber, int pageSize, string searchTerm = "");

        int ObterTotalDePessoas(string searchTerm = "");

        List<PessoasModel> BuscarTodos();

        int ObterUltimoID();

        PessoasModel BuscarCPF(string CPF);

        PessoasModel Adicionar(PessoasModel pessoas);

        PessoasModel Atualizar(PessoasModel pessoas);
    }
}
