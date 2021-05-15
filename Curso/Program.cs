using Microsoft.EntityFrameworkCore;
using System.Linq;
using CursoEFCore.Data;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using System;
using System.Collections.Generic;

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
            // InserirDadosEmMassa();
            // ConsultaDados();
            // CadastrarPedido();
            ConsultaPedidoCarregamentoAdiantado();
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

        private static void ConsultaDados()
        {
            using var db = new ApplicationContext();
            // var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes.Where(p => p.Id > 0).ToList();
            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando o cliente: {cliente.Id}");
                // db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void CadastrarPedido() 
        {
            using var db = new ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                InciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem 
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }   
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }
    
        private static void ConsultaPedidoCarregamentoAdiantado()
        {
            using var db = new ApplicationContext();
            var pedidos = db.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(p => p.Produto)
                .ToList();
                
            Console.WriteLine(pedidos.Count);
        }    
    }
}
