using System;
using Microsoft.Data.Sqlite;

namespace SqliteData
{
    public static class Class1
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDataBase.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Server (Server_Name NVARCHAR(2048) NOT NULL, " +
                   "Server_User NVARCHAR(2048) NOT NULL, " +
                   "Server_Pass NVARCHAR(2048) NOT NULL, " +
                    "Server_Db NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddData(string inputText1, string inputText2, string inputText3, string inputText4)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDataBase.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from Server;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDataBase.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Server VALUES (@Entry1,@Entry2,@Entry3,@Entry4);";
                insertCommand.Parameters.AddWithValue("@Entry1", inputText1);
                insertCommand.Parameters.AddWithValue("@Entry2", inputText2);
                insertCommand.Parameters.AddWithValue("@Entry3", inputText3);
                insertCommand.Parameters.AddWithValue("@Entry4", inputText4);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static String GetServer()
        {
            String entries = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDataBase.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Server_Name from Server", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.HasRows)
                {
                    while (query.Read())
                    {
                        entries = (query.GetString(0));
                    }
                }

                db.Close();
            }

            return entries;
        }

        public static String GetUser()
        {
            String us = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDataBase.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Server_User from Server", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.HasRows)
                {
                    while (query.Read())
                    {
                        us = (query.GetString(0));
                    }
                }

                db.Close();
            }

            return us;
        }

        public static String GetPass()
        {
            String pas = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDataBase.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Server_Pass from Server", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.HasRows)
                {
                    while (query.Read())
                    {
                        pas = (query.GetString(0));
                    }
                }

                db.Close();
            }

            return pas;
        }

        public static String GetDb()
        {
            String dbs = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDataBase.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Server_Db from Server", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.HasRows)
                {
                    while (query.Read())
                    {
                        dbs = (query.GetString(0));
                    }
                }

                db.Close();
            }

            return dbs;
        }
    }
}
