using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.DataBase
{
    public class DatabaseConfig
    {
        public const string DatabaseName = "markdownFileEntries.db";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        public const string DatabaseDir = FileManager.ROOT_DIR + "/datas/"; 

       static  public string getDataBasePath()
        {
            return DatabaseDir + DatabaseName; 
        }
    }
}
