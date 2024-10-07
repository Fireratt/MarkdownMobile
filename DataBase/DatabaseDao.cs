using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.DataBase
{
    public class DatabaseDao
    {
        private SQLiteAsyncConnection database;
        static private DatabaseDao databaseDao; 
        private DatabaseDao()
        {

        }
        // delay the initialize
        async Task InitConnection()
        {
            if(database is not null)
            {
                return;
            }
            if (!Directory.Exists(DatabaseConfig.DatabaseDir))
            {
                Directory.CreateDirectory(DatabaseConfig.DatabaseDir);   // 若存放所有markdown文档的文件夹不存在，则先创建一个。
            }
            database = new SQLiteAsyncConnection(DatabaseConfig.getDataBasePath(), DatabaseConfig.Flags);
            await database.CreateTableAsync<FileEntry>();
            return; 
        }
        public static DatabaseDao getDataBase()// secure the singleton 
        {
            if(databaseDao == null)
            {
                databaseDao = new DatabaseDao(); 
            }
            return databaseDao;
        }
        public async Task<List<FileEntry>> GetEntriesAsync()
        {
            InitConnection();
            List<FileEntry> result = await database.Table<FileEntry>().ToListAsync() ;
            return result; 
        }

        public async Task<int> InsertEntriesAsync(FileEntry entry)
        {
            InitConnection(); 
            return await database.InsertAsync(entry); 
        }
    }
}
