using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SqliteData
{
    public static class ItemClass
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Item (Item_Barcode NVARCHAR(2048) NOT NULL, " +
                    "Description NVARCHAR(2048) NOT NULL, " +
                    "Item_Number NVARCHAR(2048) NOT NULL, " +
                    "Department NVARCHAR(2048) NOT NULL, " +
                    "Category NVARCHAR(2048) NOT NULL, " +
                    "Taxes NVARCHAR(2048) NOT NULL, " +
                    "Cost NVARCHAR(2048) NOT NULL, " +
                    "Price NVARCHAR(2048) NOT NULL," +
                    "Tag NVARCHAR(2048) NOT NULL, " +
                    "Tagq NVARCHAR(2048) NOT NULL, " +
                    "Foodstamp NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddData(string inputText1, string inputText2, string inputText3, string inputText4, string inputText5, string inputText6, string inputText7, string inputText8, string inputText9, string inputText10, string inputText11)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from Item;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Item VALUES (@Entry1,@Entry2,@Entry3,@Entry4,@Entry5,@Entry6,@Entry7,@Entry8,@Entry9,@Entry10,@Entry11);";
                insertCommand.Parameters.AddWithValue("@Entry1", inputText1);
                insertCommand.Parameters.AddWithValue("@Entry2", inputText2);
                insertCommand.Parameters.AddWithValue("@Entry3", inputText3);
                insertCommand.Parameters.AddWithValue("@Entry4", inputText4);
                insertCommand.Parameters.AddWithValue("@Entry5", inputText5);
                insertCommand.Parameters.AddWithValue("@Entry6", inputText6);
                insertCommand.Parameters.AddWithValue("@Entry7", inputText7);
                insertCommand.Parameters.AddWithValue("@Entry8", inputText8);
                insertCommand.Parameters.AddWithValue("@Entry9", inputText9);
                insertCommand.Parameters.AddWithValue("@Entry10", inputText10);
                insertCommand.Parameters.AddWithValue("@Entry11", inputText11);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static String GetItemBarcode()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Item_Barcode from Item", db);

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


        public static String GetDescription()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Description from Item", db);

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

        public static String GetItemNumber()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Item_Number from Item", db);

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

        public static String GetDepartment()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Department from Item", db);

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

        public static String GetCategory()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Category from Item", db);

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

        public static String GetTax()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Taxes from Item", db);

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

        public static String GetCost()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Cost from Item", db);

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

        public static String GetPrice()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Price from Item", db);

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

        public static String GetTag()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Tag from Item", db);

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

        public static String GetTagq()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Tagq from Item", db);

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

        public static String GetFoodstamp()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteItemAct.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Foodstamp from Item", db);

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
