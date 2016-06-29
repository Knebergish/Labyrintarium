using System;
using System.IO;

using System.Data;
using System.Data.SQLite;

namespace TestOpenGL.DataIO
{
    class DataBaseIO
    {
        public string nameDataBase = "dataBase.db";
        private SQLiteConnection conn;
        private string path;

        public DataBaseIO(string path)
        {
            this.path = path;
            try
            {
                InitDB();
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void InitDB()
        {
            if(!File.Exists(this.path + "\\" + this.nameDataBase))
            {
                throw new FileNotFoundException("Файл игровой базы данных не найден. проверьте наличие файла " 
                    + this.nameDataBase 
                    + " в одной папке с исполняемым файлом игры.");
            }

            try
            {
                this.conn = new SQLiteConnection(string.Format("Data Source=" + this.nameDataBase));
                conn.Open();
                conn.Close();
            }
            catch
            {
                throw new FileLoadException("Файл игровой базы данных найден, но не может быть загружен. Проверьте его доступность.");
            }
        }

        public DataTable ExecuteSQL(string sql)
        {
            conn.Open();
            SQLiteCommand sqliteCommand = new SQLiteCommand(this.conn);
            sqliteCommand.CommandText = sql;
            DataTable dt = new DataTable();

            try
            {
                SQLiteDataReader sqliteReader = sqliteCommand.ExecuteReader();
                dt.Load(sqliteReader);
                sqliteReader.Close();
            }
            catch
            {
                throw new DataException("Ошибка чтения данных из игровой базы данных.");
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        /*~DataBaseIO()
        {
            this.conn.Close();
        }*/
    }
}
