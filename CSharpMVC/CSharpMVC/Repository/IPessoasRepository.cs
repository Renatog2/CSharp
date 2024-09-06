using CSharpMVC.Models;

namespace CSharpMVC.Repository
{
    public interface IPessoasRepository
    {
        PessoasModel BuscarPessoa(int ID);

        List<PessoasModel> BuscarTodos();

        int ObterUltimoID();

        PessoasModel Adicionar(PessoasModel pessoas);

        PessoasModel Atualizar(PessoasModel pessoas);
    }
}
