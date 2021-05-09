using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CursoEFCore.Data;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();
            var exists = db.Database.GetPendingMigrations().Any();
            if (exists) 
            {
                // ..
            }
        }
    }
}
