using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SqliteData
{
    public static class MoreClass
    {

        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteMoreAct.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS More (PItem NVARCHAR(2048) NOT NULL, " +
                    "CQty NVARCHAR(2048) NOT NULL, " +
                    "Mi NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddData(string inputText1, string inputText2, string inputText3)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteMoreAct.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from More;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteMoreAct.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO More VALUES (@Entry1,@Entry2,@Entry3);";
                insertCommand.Parameters.AddWithValue("@Entry1", inputText1);
                insertCommand.Parameters.AddWithValue("@Entry2", inputText2);
                insertCommand.Parameters.AddWithValue("@Entry3", inputText3);
                

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

       
        public static String GetPItem()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteMoreAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT PItem from More", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.HasRows)
                {
                    while (query.Read())
                    {
                        bc = (query.GetString(0));
                    }
                }

                db.Close();
            }

            return bc;
        }
        public static String GetCQty()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteMoreAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT CQty from More", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.HasRows)
                {
                    while (query.Read())
                    {
                        bc = (query.GetString(0));
                    }
                }

                db.Close();
            }

            return bc;
        }
       
        public static String GetMi()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteMoreAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Mi from More", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.HasRows)
                {
                    while (query.Read())
                    {
                        bc = (query.GetString(0));
                    }
                }

                db.Close();
            }

            return bc;
        }
    }
}
