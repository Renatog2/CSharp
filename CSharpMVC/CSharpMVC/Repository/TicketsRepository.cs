using CSharpMVC.Data;
using CSharpMVC.Models;

namespace CSharpMVC.Repository
{
    public class TicketsRepository : ITicketsRepository
    {
        // Conectar o Repositório com o Banco
        private readonly DBContext _dbContext;

        public TicketsRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }


        // Editar Ticket
        public TicketsModel BuscarTicket(int ID)
        {
            return _dbContext.Tickets.FirstOrDefault(x => x.ID == ID);
        }
        public TicketsModel Atualizar(TicketsModel tickets)
        {
            TicketsModel ticketDB = BuscarTicket(tickets.ID);

            if (ticketDB == null) throw new Exception("Ocorreu um erro interno");

            ticketDB.FK_IDPessoas = tickets.FK_IDPessoas;
            ticketDB.Quantidade = tickets.Quantidade;
            ticketDB.Situacao = tickets.Situacao;
            ticketDB.DataDeEntrega = tickets.DataDeEntrega;

            _dbContext.Tickets.Update(ticketDB);
            _dbContext.SaveChanges();
            return ticketDB;
        }

        // Obter Ticket
        public List<TicketsModel> BuscarTodos()
        {
            return _dbContext.Tickets.ToList();
        }


        // Criar Ticket
        public TicketsModel Adicionar(TicketsModel tickets)
        {
            _dbContext.Tickets.Add(tickets);
            _dbContext.SaveChanges();
            return tickets;
        }


        // Obter o ultimo ID em uso
        public int ObterUltimoID()
        {
            var ultimoID = _dbContext.Tickets.OrderByDescending(p => p.ID).FirstOrDefault();
            return ultimoID != null ? ultimoID.ID : 0;
        }

    }
}
