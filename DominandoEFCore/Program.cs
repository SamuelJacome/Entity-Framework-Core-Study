using System.Net.Mime;
using System;
using DominandoEFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using DominandoEFCore.Domain;
using System.Linq;

namespace DominandoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // EnsureCreated();
            // EnsureDeleted();
            // GapDoEnsureCreated();
            // HealthCheckDataBase();
            // _count = 0;
            // ManagerStateConnection(false);
            // _count = 0;
            // ManagerStateConnection(true);
            // ExecuteSql();
            // SqlInjection();
            // PendingMigrations();
            ScriptGeralDoBancoDeDados();
        }
        static int _count;
        static void EnsureCreated()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();
        }
        static void EnsureDeleted()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
        }
        static void GapDoEnsureCreated()
        {
            using var db1 = new ApplicationContext();
            using var db2 = new ApplicationContextCity();
            db1.Database.EnsureCreated();
            db2.Database.EnsureCreated();

            var databaseCreator = db2.GetService<IRelationalDatabaseCreator>();
            databaseCreator.CreateTables();
        }
        static void HealthCheckDataBase()
        {
            //Opção 1
            using var db = new ApplicationContext();
            //Metodo do EF para validar a conexão ao BD
            var canConnect = db.Database.CanConnect();

            if (canConnect)
            {
                Console.WriteLine("Ok");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }


            //Opção 2
            // try
            // {
            //Primeira possibilidade de teste 
            // var connection = db.Database.GetDbConnection();
            // connection.Open();

            //Segunda possibilidade 
            // db.Departaments.AnyAsync();
            // Console.WriteLine("Posso me conectar");
            // }
            // catch
            // {
            //     Console.WriteLine("Não Posso me conectar");
            // }

        }
        static void ManagerStateConnection(bool managerStateConnection)
        {
            using var db = new ApplicationContext();
            var time = System.Diagnostics.Stopwatch.StartNew();

            var connection = db.Database.GetDbConnection();
            connection.StateChange += (_, __) => ++_count;
            if (managerStateConnection)
            {
                connection.Open();
            }
            for (var i = 0; i < 200; i++)
            {
                db.Departaments.AsNoTracking().AnyAsync();
            }

            time.Stop();
            var message = $"Tempo: {time.Elapsed.ToString()}, {managerStateConnection}, Contador: {_count}";
            Console.WriteLine(message);
        }
        static void ExecuteSql()
        {
            using var db = new ApplicationContext();

            //Primeira opção

            using (var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT 1";
                cmd.ExecuteNonQuery();
            }

            //Segunda opção - Evita SQL Injection
            var description = "Teste";
            db.Database.ExecuteSqlRaw("update departaments set description={0} where id=1", description);

            //Terceira Opção
            db.Database.ExecuteSqlInterpolated($"update departaments set description={description} where id=1");


        }
        static void SqlInjection()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Departaments.AddRange(
                new Departament
                {
                    Description = "Departamento 01"
                },
                new Departament
                {
                    Description = "Departamento 02"
                });

            db.SaveChanges();

            var description = "Teste ' or 1='1";
            //right
            db.Database.ExecuteSqlRaw("update departaments set description='AtaqueSqlInjection' where description={0}", description);
            //wrong
            // db.Database.ExecuteSqlRaw($"update departaments set descricao='AtaqueSqlInjection' where description='{description}'");
            foreach (var departament in db.Departaments.AsNoTracking())
            {
                Console.WriteLine($"Id: {departament.Id}, Descricao: {departament.Description}");
            }
        }
        static void PendingMigrations()
        {
            using var db = new ApplicationContext();

            var pendingMigrations = db.Database.GetPendingMigrations();

            Console.WriteLine($"Total: {pendingMigrations.Count()}");

            foreach (var migration in pendingMigrations)
            {
                Console.WriteLine($"Migração: {migration}");
            }
        }
        static void AppMigrations()
        {
            using var db = new ApplicationContext();

            db.Database.Migrate();
        }
        static void MigrationsAlreadyApp()
        {
            using var db = new ApplicationContext();

            var migrations = db.Database.GetAppliedMigrations();

            Console.WriteLine($"Total: {migrations.Count()}");

            foreach (var migration in migrations)
            {
                Console.WriteLine($"Migração: {migration}");
            }
        }
        static void ScriptGeralDoBancoDeDados()
        {
            using var db = new ApplicationContext();
            var script = db.Database.GenerateCreateScript();

            Console.WriteLine(script);
        }
    // Módulo de Infraestrutura

        static void SearchDepartaments ()
        {
            using var db = new
        }

    }
}
