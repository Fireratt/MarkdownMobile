using MauiApp1.Utils;
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
            //await database.DropTableAsync<FileEntry>();
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
            await InitConnection();
            List<FileEntry> result = await database.Table<FileEntry>().ToListAsync() ;
            return result; 
        }

        public async Task<int> InsertEntriesAsync(FileEntry entry)
        {
            await InitConnection();
            FileEntry result = (await database.FindAsync<FileEntry>(entry.Path));
            if (result != null)
            {
                Console.WriteLine("Result:" + result.Path); 
                return await database.UpdateAsync(entry);
            } 
            return await database.InsertAsync(entry); 
        }

        public async Task<int> InsertEntryByPathAsync(string path)
        {
            return await InsertEntriesAsync(new FileEntry{Name = StringUtils.GetFileName(path) , Path = path});
        }
    }
}
