using Microsoft.EntityFrameworkCore;
using System.Linq;
using CursoEFCore.Data;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using System;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // using var db = new ApplicationContext();
            // var exists = db.Database.GetPendingMigrations().Any();
            // if (exists) 
            // {
            //     // ..
            // }

            // InserirDados();
            InserirDadosEmMassa();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1231343143",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new ApplicationContext();
            db.Produtos.Add(produto);
            // db.Set<Produto>().Add(produto);
            // db.Entry(produto).State = EntityState.Added;
            // db.Add(produto);
            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: {registros}");
        }

        private static void InserirDadosEmMassa() 
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "322154545",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Renato Francisco",
                CEP = "99999000",
                Cidade = "São Paulo",
                Estado = "SP",
                Telefone = "11999999999"
            };

            using var db = new ApplicationContext();
            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            System.Console.WriteLine($"Total registros: {registros}");
        }
    }
}
