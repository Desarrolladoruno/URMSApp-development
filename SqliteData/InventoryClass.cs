using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SqliteData
{
    public class InventoryClass
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePhysicalInventory.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Inventory (Inventory_Id NVARCHAR(2048) NOT NULL, " +
                    "Code NVARCHAR(2048) NOT NULL, " +
                    "Store_Id NVARCHAR(2048) NOT NULL, " +
                     "Description NVARCHAR(2048) NOT NULL, " +
                    "Opened_Date NVARCHAR(2048) NOT NULL, " +
                    "Status NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddInventory(string inputText1, string inputText2, string inputText3, string inputText4, string inputText5, string inputText6)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePhysicalInventory.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from Inventory;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePhysicalInventory.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Inventory VALUES (@Entry1,@Entry2,@Entry3,@Entry4,@Entry5,@Entry6);";
                insertCommand.Parameters.AddWithValue("@Entry1", inputText1);
                insertCommand.Parameters.AddWithValue("@Entry2", inputText2);
                insertCommand.Parameters.AddWithValue("@Entry3", inputText3);
                insertCommand.Parameters.AddWithValue("@Entry4", inputText4);
                insertCommand.Parameters.AddWithValue("@Entry5", inputText5);
                insertCommand.Parameters.AddWithValue("@Entry6", inputText6);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static String GetInventoryId()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePhysicalInventory.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Inventory_Id from Inventory", db);

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

        public static String GetCode()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePhysicalInventory.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Code from Inventory", db);

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

        public static String GetStoreId()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePhysicalInventory.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Store_Id from Inventory", db);

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

        public static String GetInventoryDescription()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePhysicalInventory.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Description from Inventory", db);

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

        public static String GetOpenedDate()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePhysicalInventory.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Opened_Date from Inventory", db);

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

        public static String GetStatus()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePhysicalInventory.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Status from Inventory", db);

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
