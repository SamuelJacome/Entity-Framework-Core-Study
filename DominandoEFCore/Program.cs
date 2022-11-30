using System;
using DominandoEFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

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
            ManagerStateConnection(true);
        }

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
            if (managerStateConnection)
            {
                connection.Open();
            }
            for (var i = 0; i < 200; i++)
            {
                db.Departaments.AsNoTracking().AnyAsync();
            }

            time.Stop();
            var message = $"Tempo: {time.Elapsed.ToString()}, {managerStateConnection}";
            Console.WriteLine(message);
        }
    }
}
