using System;
using System.Data;
using System.Data.SQLite;

namespace MiniChat.Data
{
    public class DatabaseHelper
    {
        private SQLiteConnection conn;

        public DatabaseHelper()
        {
            string dbPath = "Data Source=MiniChat.db;Version=3;";
            conn = new SQLiteConnection(dbPath);
            conn.Open();

            CreateTables();
        }

        private void CreateTables()
        {
            string usersTable = @"CREATE TABLE IF NOT EXISTS Users (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Name TEXT
                                  );";

            string messagesTable = @"CREATE TABLE IF NOT EXISTS Messages (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        UserId INTEGER,
                                        Message TEXT,
                                        Timestamp DATETIME
                                     );";

            using (SQLiteCommand cmd = new SQLiteCommand(usersTable, conn))
                cmd.ExecuteNonQuery();

            using (SQLiteCommand cmd = new SQLiteCommand(messagesTable, conn))
                cmd.ExecuteNonQuery();
        }

        public void AddUser(string name)
        {
            string query = "INSERT INTO Users (Name) VALUES (@name)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetUsers()
        {
            string query = "SELECT * FROM Users";
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn))
                adapter.Fill(dt);
            return dt;
        }

        public void AddMessage(int userId, string message)
        {
            string query = "INSERT INTO Messages (UserId, Message, Timestamp) VALUES (@uid, @msg, @time)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@msg", message);
                cmd.Parameters.AddWithValue("@time", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetMessages(int userId)
        {
            string query = "SELECT * FROM Messages WHERE UserId=@uid ORDER BY Timestamp ASC";
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@uid", userId);
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
