using System;
using CPL_Elo.database;

namespace BuildDatabase
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            EloContext eloContext = new EloContext();
            eloContext.Database.EnsureCreated();
        }
    }
}
