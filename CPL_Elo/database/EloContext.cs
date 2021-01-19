using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CPL_Elo.database
{
    public class EloContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            string currentPath = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}";
            // allows the application to find the database file
            currentPath = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                $"{Path.DirectorySeparatorChar}{currentPath}" :
                currentPath;

            Console.WriteLine(Path.Join(currentPath, "Database", "cpl-data.db"));
            string databaseDirectory = Path.Join(currentPath, "Database");
            Directory.CreateDirectory(databaseDirectory);
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = Path.Join(databaseDirectory, "cpl-data.db") };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_dynamic_cdecl());
            connection.Open();
            options.UseSqlite(connection);
            base.OnConfiguring(options);
            // options.UseNpgsql(connection);
        }
    }
}
