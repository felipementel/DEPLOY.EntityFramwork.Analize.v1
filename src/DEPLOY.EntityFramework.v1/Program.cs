using Bogus;
using Bogus.Extensions.Brazil;
using DEPLOY.EntityFramework.v1.Domain;
using DEPLOY.EntityFramework.v1.Infra.Database;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace DEPLOY.EntityFramework.v1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int quantidadeContratos = 300;
            int quantidadePessoaContrato = 100;

            DeployDbContext dbContext = new();
            var contractId = Guid.NewGuid();

            var contratoFaker = new Faker<Contrato>(locale: "pt_BR")
                           .CustomInstantiator(f =>
                           {
                               return new Contrato(
                                   id: f.Random.Guid(),
                                   createdAt: DateTime.Now.ToUniversalTime(),
                                   updatedAt: DateTime.Now.ToUniversalTime(),
                                   numero: f.Random.Long(int.MinValue, int.MaxValue),
                                   dataInicio: f.Date.RecentDateOnly(),
                                   dataFim: f.Date.FutureDateOnly(),
                                   ativo: f.Random.Bool()
                               );
                           }).FinishWith((f, u) =>
                           {
                               Console.WriteLine("Contrato criado com id {0}", u.Id);
                           })
                           .Generate(quantidadeContratos);

            var PessoaFaker =
                new Faker<Pessoa>(locale: "pt_BR").CustomInstantiator(p =>
                {
                    return new Pessoa(
                        p.Random.Guid(),
                        createdAt: DateTime.Now.ToUniversalTime(),
                        DateTime.Now.ToUniversalTime(),
                        p.Person.FullName,
                        p.Person.Email,
                        p.Person.Phone,
                        p.Person.Cpf(),
                        p.Person.Address.Street,
                        p.Date.PastDateOnly()
                        );
                }).FinishWith((f, u) =>
                {
                    Console.WriteLine("Pessoa criado com id {0}", u.Id);
                })
                .Generate(quantidadeContratos);


            dbContext.Pessoas.AddRange(PessoaFaker);
            await dbContext.SaveChangesAsync();


            dbContext.Contratos.AddRange(contratoFaker);
            await dbContext.SaveChangesAsync();

            //selecionar pessoas aleatorias e incluir em contrato
            dbContext.Pessoas
                .OrderBy(p => Guid.NewGuid())
                .Take(4)
                .ToList()
                .ForEach(p =>
                {
                    contratoFaker[0].Interessados.Add(p);
                });

            //dbContext.Cont

            //Adicionar pessoas aleatorias em contratopessoa



            // dbContext.PessoaContratos.AddRange(
            //     contratoFaker.SelectMany(c => PessoaFaker.Select(p => new PessoaContrato
            //     {
            //         ContratoId = c.Id,
            //         PessoaId = p.Id
            //     })));
            // await dbContext.SaveChangesAsync();

            var position = 0;

            var nextPage = dbContext.Pessoas
                .OrderBy(b => b.CreatedAt)
                .Skip(position * 10)
                .Take(10)
                .ToList();

            var contractId2 = dbContext.Contratos.FromSql($"SELECT TOP 1 ContratoId FROM Contratos ORDER BY CreatedAt DESC");
            var contrato = dbContext.Contratos.FromSql($"SELECT * FROM Contratos WHERE ContratoId = {contractId}").ToList();

            var user = new Microsoft.Data.SqlClient.SqlParameter("user", "johndoe");
            var blogs = dbContext.Pessoas
                .FromSql($"EXECUTE dbo.GetMostPopularBlogsForUser @filterByUser={user}")
                .ToList();

            //DbContext.Add<Contrato>(contratoFaker[0]);

            //dbContext.Entry<Contrato>(contratoFaker[0]).State = EntityState.Added;
            // await dbContext.SaveChangesAsync();

            var item1 = dbContext.Pessoas.OrderDescending();

            var item2 = dbContext.Pessoas.AsSplitQuery().OrderDescending();

            var item21 = dbContext.Pessoas.AsSplitQuery().OrderByDescending(p => p.Nome);

            var item3 = dbContext.Pessoas.AsNoTracking().OrderDescending();

            var item4 = dbContext.Pessoas.AsNoTracking().AsSplitQuery().OrderDescending();

            var item5 = dbContext.Pessoas.AsNoTracking().AsSplitQuery().AsSingleQuery().OrderDescending();

            var item6 = dbContext.Pessoas.Where(p => p.Nome.StartsWith("Ma")).ExecuteUpdate(s => s.SetProperty(p => p.Nome, "NOVO NOME"));
        }
    }
}
