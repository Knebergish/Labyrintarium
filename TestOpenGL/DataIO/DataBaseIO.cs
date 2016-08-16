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
        //-------------


        public DataBaseIO(string path)
        {
            this.path = path;
            InitDB();
        }
        //=============


        private void InitDB()
        {
            string fullName = path + "\\" + nameDataBase;
            if (!File.Exists(fullName))
            {
                ExceptionAssistant.NewException(new FileNotFoundException("Файл игровой базы данных не найден. проверьте наличие файла "
                    + nameDataBase
                    + " в одной папке с исполняемым файлом игры."));
            }

            try
            {
                conn = new SQLiteConnection(string.Format("Data Source=" + fullName));
                conn.Open();
                conn.Close();
            }
            catch
            {
                ExceptionAssistant.NewException(new FileLoadException("Файл игровой базы данных найден, но не может быть загружен. Проверьте его доступность."));
            }
        }

        public DataTable ExecuteSQL(string sql)
        {
            conn.Open();
            SQLiteCommand sqliteCommand = new SQLiteCommand(conn);
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
                ExceptionAssistant.NewException(new DataException("Ошибка чтения данных из игровой базы данных."));
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}
