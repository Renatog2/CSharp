﻿using CSharpMVC.Data;
using CSharpMVC.Models;

namespace CSharpMVC.Repository
{
    public class PessoasRepository : IPessoasRepository
    {
        // Conectar o Repositório com o Banco
        private readonly DBContext _dbContext;

        public PessoasRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }


        // Editar Pessoa
        public PessoasModel BuscarPessoa(int ID)
        {
            return _dbContext.Pessoas.FirstOrDefault(x => x.ID == ID);
        }
        public PessoasModel Atualizar(PessoasModel pessoas)
        {
            PessoasModel pessoasDB = BuscarPessoa(pessoas.ID);

            if (pessoasDB == null) throw new Exception("Ocorreu um erro interno");

            pessoasDB.Nome = pessoas.Nome;
            pessoasDB.CPF = pessoas.CPF;
            pessoasDB.Situacao = pessoas.Situacao;
            pessoasDB.DataDeAlteracao = pessoas.DataDeAlteracao;

            _dbContext.Pessoas.Update(pessoasDB);
            _dbContext.SaveChanges();
            return pessoasDB;
        }


        // Obter Pessoa
        public List<PessoasModel> BuscarTodos()
        {
            return _dbContext.Pessoas.ToList();
        }
        public IEnumerable<PessoasModel> ObterPessoasPaginadas(int pageNumber, int pageSize, string searchTerm = "")
        {
            var query = _dbContext.Pessoas.AsQueryable();

            // Busca pelo nome da pessoa
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Nome.Contains(searchTerm));
            }
            return query.OrderBy(p => p.ID).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
        public int ObterTotalDePessoas(string searchTerm = "")
        {
            var query = _dbContext.Pessoas.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Nome.Contains(searchTerm));
            }
            return query.Count();
        }


        // Criar Pessoa
        public PessoasModel Adicionar(PessoasModel pessoas)
        {
            _dbContext.Pessoas.Add(pessoas);
            _dbContext.SaveChanges();
            return pessoas;
        }


        // Obter o ultimo ID em uso
        public int ObterUltimoID()
        {
            var ultimoID = _dbContext.Pessoas.OrderByDescending(p => p.ID).FirstOrDefault();
            return ultimoID != null ? ultimoID.ID : 0;
        }


        // Verifica CPF dubplicado
        public PessoasModel BuscarCPF(string CPF)
        {
            return _dbContext.Pessoas.FirstOrDefault(x => x.CPF == CPF);
        }
    }
}
