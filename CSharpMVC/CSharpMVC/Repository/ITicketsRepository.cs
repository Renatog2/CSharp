using CSharpMVC.Models;

namespace CSharpMVC.Repository
{
    public interface ITicketsRepository
    {
        TicketsModel BuscarTicket(int ID);

        List<TicketsModel> BuscarTodos();

        int ObterUltimoID();

        TicketsModel Adicionar(TicketsModel tickets);

        TicketsModel Atualizar(TicketsModel tickets);
    }
}
