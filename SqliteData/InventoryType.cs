using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteData
{
    public static class InventoryType
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteInventoryType.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Inventorytype (Type NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddInventoryType(string inputText1)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteInventoryType.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from Inventorytype;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteInventoryType.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Inventorytype VALUES (@Entry1);";
                insertCommand.Parameters.AddWithValue("@Entry1", inputText1);


                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static String GetInventoryType()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteInventoryType.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Type from Inventorytype", db);

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
