using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pedidos.API.Infrastructure;
using Pedidos.API.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pedidos.API
{
    internal class SeedData
    {
        internal static async Task EnsureSeedData(IServiceProvider services)
        {
            using (var scope = services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EcommContext>();

                await CreateTables(context);

                await SaveViewers(context);

                await SaveEquipes(context);

                await SaveProdutos(context);

                await SavePedidos(context);

                await SaveEncomendas(context);
            }
        }



        private static async Task CreateTables(EcommContext context)
        {
            await CreateTableViewers(context);

            await CreateTableProdutos(context);

            await CreateTableEquipes(context);

            await CreateTablePedidos(context);

            await CreateTablePedidosProdutos(context);

            await CreateTableEncomendas(context);
        }

        private static async Task CreateTableViewers(EcommContext context)
        {
            var sql
                = @"CREATE TABLE IF NOT EXISTS 
                        'Viewers' (
                            'Id'    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            'Username'  TEXT NOT NULL,
                            'Password'  TEXT NOT NULL,
                            'Role'      TEXT NOT NULL
                        );";

            await ExecuteSqlCommandAsync(context, sql);
        }

        private static async Task CreateTablePedidosProdutos(EcommContext context)
        {
            var sql
                = @"CREATE TABLE IF NOT EXISTS 
                        'PedidosProdutos' (
                            'Id'    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            'PedidoId'  INTEGER NOT NULL,
                            'ProdutoId'  INTEGER NOT NULL
                        );";

            await ExecuteSqlCommandAsync(context, sql);
        }

        private static async Task CreateTableEquipes(EcommContext context)
        {
            var sql
                = @"CREATE TABLE IF NOT EXISTS 
                        'Equipes' (
                            'Id'    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            'Nome'  TEXT NOT NULL,
                            'Descricao'  TEXT NOT NULL
                        );";

            await ExecuteSqlCommandAsync(context, sql);
        }

        private static async Task CreateTableEncomendas(EcommContext context)
        {
            var sql
                = @"CREATE TABLE IF NOT EXISTS 
                        'Encomendas' (
                            'Id'        INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            'PlacaVeiculo'  TEXT NOT NULL,
                            'PedidoId'  INTEGER NOT NULL,
                            'EquipeId'  INTEGER NOT NULL,
                                 FOREIGN KEY(PedidoId) REFERENCES Pedidos(Id),
                                 FOREIGN KEY(EquipeId) REFERENCES Equipes(Id)
                        );";

            await ExecuteSqlCommandAsync(context, sql);
        }

        private static async Task CreateTablePedidos(EcommContext context)
        {
            var sql
                = @"CREATE TABLE IF NOT EXISTS 
                        'Pedidos' (
                            'Id'               INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            'DataCriacao'      DATETIME NOT NULL,
                            'DataEntrega'      DATETIME NULL,
                            'Endereco'         TEXT NOT NULL,
                            'NumeroIdentificacao'   TEXT NOT NULL
                        );";

            await ExecuteSqlCommandAsync(context, sql);
        }

        private static async Task CreateTableProdutos(EcommContext context)
        {
            var sql
                = @"CREATE TABLE IF NOT EXISTS 
                        'Produtos' (
                            'Id'        INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                        'Nome'	    TEXT NOT NULL,
                            'Descricao'      TEXT NOT NULL,
	                        'Valor'     NUMERIC NOT NULL
                        );";

            await ExecuteSqlCommandAsync(context, sql);
        }

        private static async Task ExecuteSqlCommandAsync(EcommContext context, string createTableSql)
        {
            var result = await context.Database.ExecuteSqlRawAsync(createTableSql);
        }

        private static async Task SaveViewers(EcommContext context)
        {
            var vwDbSet = context.Set<Viewer>();

            if (!vwDbSet.Any())
            {

                await vwDbSet.AddAsync(
                    new Viewer
                    {
                        Username = "Ecommerce",
                        Password = "Ecommerce",
                        Role = "Admin"
                    });

                await context.SaveChangesAsync();
            }
        }

        private static async Task SaveProdutos(EcommContext context)
        {
            var produtoDbSet = context.Set<Produto>();

            var livros = await GetLivros();

            if (!produtoDbSet.Any())
            {
                foreach (var livro in livros)
                {
                    await produtoDbSet.AddAsync(new Produto(livro.Nome, livro.Descricao, livro.Preco));
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task SaveEquipes(EcommContext context)
        {
            var equipeDbSet = context.Set<Equipe>();

            if (!equipeDbSet.Any())
            {
                await equipeDbSet.AddAsync(new Equipe("Vendas", "Equipe de vendas especializados na venda de livros"));

                await equipeDbSet.AddAsync(new Equipe("Compras", "Equipe de compras especializados na venda de compras"));

                await equipeDbSet.AddAsync(
                    new Equipe("TI", "Equipe de TI especializados na gestão de infra e softwares de uso interno da empresa"));

                await context.SaveChangesAsync();
            }
        }

        private static async Task SavePedidos(EcommContext context)
        {
            var pedidosDbSet = context.Set<Pedido>();
            var produtosDbSet = context.Set<Produto>();

            if (!pedidosDbSet.Any())
            {
                var cincoPrimeirosProdutos = produtosDbSet.Take(5).ToList();
                var entre5e10Produtos = produtosDbSet.Skip(5).Take(5).ToList();
                var ultimosProdutos = produtosDbSet.Skip(10).Take(5).ToList();

                DateTime doisDiasDepois = DateTime.Now.AddDays(2);
                DateTime cincoDiasDepois = DateTime.Now.AddDays(5);
                DateTime quatorzeDiasDepois = DateTime.Now.AddDays(14);

                await pedidosDbSet.AddAsync(new Pedido("001",
                                            doisDiasDepois,
                                            "Beco Barracão 2 ,Aeroporto Velho,Rio Branco - AC",
                                            cincoPrimeirosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("002",
                                            cincoDiasDepois,
                                            "Quadra Quadra 32 ,Jardim América III,Goiânia - GO",
                                            entre5e10Produtos));

                await pedidosDbSet.AddAsync(new Pedido("003",
                                            quatorzeDiasDepois,
                                            "Rua A-51 233,Parque Sagrada Família,Rondonópolis - MT",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("004",
                                            quatorzeDiasDepois,
                                            "Rua Tocantins 123,Rio Marinho,Cariacica - ES",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("005",
                                            quatorzeDiasDepois,
                                            "Rua Iara 1234,Paraíso,Belo Horizonte - MG",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("006",
                                            quatorzeDiasDepois,
                                            "Rua Trinta e Seis 333,Viviane,Redenção - PA",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("007",
                                            quatorzeDiasDepois,
                                            "Rua Imbituva 11,Cajuru,Curitiba - PR",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("008",
                                            quatorzeDiasDepois,
                                            "Rua Pirapitinga 123,Bangu,Rio de Janeiro - RJ",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("009",
                                            quatorzeDiasDepois,
                                            "Rua Travessa Florisbela 345,Barreto,Niterói - RJ",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("010",
                                            quatorzeDiasDepois,
                                            "Rua Expedicionário Felício Tomazini 312,Maria Paula,São Gonçalo - RJ",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("011",
                                            quatorzeDiasDepois,
                                            "Rua Estrada Diogo Moreira 123,Vila Santa Teresa,Belford Roxo - RJ",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("012",
                                            doisDiasDepois,
                                            "Rua Luiz Fernando Veríssimo 2 ,Vila Acre,Rio Branco - AC",
                                            cincoPrimeirosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("013",
                                            cincoDiasDepois,
                                            "Rua Elizabeth Áurea de Souza 11 ,Nova Lima,Campo Grande - MS",
                                            entre5e10Produtos));

                await pedidosDbSet.AddAsync(new Pedido("014",
                                            quatorzeDiasDepois,
                                            "Rua Guia Lopes 30,Jardim São João 2ª Seção,Ponta Porã - MS",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("015",
                                            quatorzeDiasDepois,
                                            "Rua Tocantins 123,Rio Marinho,Cariacica - MS",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("016",
                                            quatorzeDiasDepois,
                                            "Rua Lina Bardi 11,Residencial Vida Nova III,Campo Grande - MS",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("017",
                                            quatorzeDiasDepois,
                                            "Rua Trinta e Seis 333,Viviane,Redenção - PA",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("018",
                                            quatorzeDiasDepois,
                                            "Rua Imbituva 11,Cajuru,Curitiba - PR",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("019",
                                            quatorzeDiasDepois,
                                            "Rua Pirapitinga 123,Bangu,Rio de Janeiro - RJ",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("020",
                                            quatorzeDiasDepois,
                                            "Rua Travessa Florisbela 345,Barreto,Niterói - RJ",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("021",
                                         doisDiasDepois,
                                         "Beco Barracão 2 ,Aeroporto Velho,Rio Branco - AC",
                                         cincoPrimeirosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("022",
                                            cincoDiasDepois,
                                            "Quadra Quadra 32 ,Jardim América III,Goiânia - GO",
                                            entre5e10Produtos));

                await pedidosDbSet.AddAsync(new Pedido("023",
                                            quatorzeDiasDepois,
                                            "Rua A-51 233,Parque Sagrada Família,Rondonópolis - MT",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("024",
                                            quatorzeDiasDepois,
                                            "Rua Tocantins 123,Rio Marinho,Cariacica - ES",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("025",
                                            quatorzeDiasDepois,
                                            "Rua Iara 1234,Paraíso,Belo Horizonte - MG",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("026",
                                            quatorzeDiasDepois,
                                            "Rua Trinta e Seis 333,Viviane,Redenção - PA",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("027",
                                            quatorzeDiasDepois,
                                            "Rua Imbituva 11,Cajuru,Curitiba - PR",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("028",
                                            quatorzeDiasDepois,
                                            "Rua Pirapitinga 123,Bangu,Rio de Janeiro - RJ",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("029",
                                            quatorzeDiasDepois,
                                            "Rua Travessa Florisbela 345,Barreto,Niterói - RJ",
                                            ultimosProdutos));

                await pedidosDbSet.AddAsync(new Pedido("030",
                                            quatorzeDiasDepois,
                                            "Rua Expedicionário Felício Tomazini 312,Maria Paula,São Gonçalo - RJ",
                                            ultimosProdutos));

                await context.SaveChangesAsync();
            }
        }

        private static async Task SaveEncomendas(EcommContext context)
        {
            var encomendasDbSet = context.Set<Encomenda>();
            var pedidoDbSet = context.Set<Pedido>();
            var equipeDbSet = context.Set<Equipe>();

            if (!encomendasDbSet.Any() && pedidoDbSet.Any())
            {
                var equipeVendas = equipeDbSet.First(o => o.Nome == "Vendas");
                var equipeCompras = equipeDbSet.First(o => o.Nome == "Compras");
                var equipeTI = equipeDbSet.First(o => o.Nome == "TI");

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "001").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id, 
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "002").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeTI.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "003").Id, "KLZ-3034"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "004").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "005").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeTI.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "006").Id, "KLZ-3034"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "007").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "008").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeTI.Id, 
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "009").Id, "KLZ-3034"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "010").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "011").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "012").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "013").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeTI.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "014").Id, "KLZ-3034"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "015").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "016").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeTI.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "017").Id, "KLZ-3034"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "018").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "019").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeTI.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "020").Id, "KLZ-3034"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                   pedidoDbSet.First(o => o.NumeroIdentificacao == "021").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "022").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeTI.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "023").Id, "KLZ-3034"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "024").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "025").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeTI.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "026").Id, "KLZ-3034"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "027").Id, "MSZ-8222"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeCompras.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "028").Id, "JEL-5881"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeTI.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "029").Id, "KLZ-3034"));

                await encomendasDbSet.AddAsync(new Encomenda(equipeVendas.Id,
                    pedidoDbSet.First(o => o.NumeroIdentificacao == "030").Id, "MSZ-8222"));

                await context.SaveChangesAsync();
            }
        }

        static async Task<List<Livro>> GetLivros()
        {
            var json = await File.ReadAllTextAsync("livros.json");

            return JsonConvert.DeserializeObject<List<Livro>>(json);
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}