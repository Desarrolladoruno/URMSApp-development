using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SqliteData
{
    public static class DiscountClass
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Discount (MMSchema NVARCHAR(2048) NOT NULL, " +
                    "XYSchema NVARCHAR(2048) NOT NULL, " +
                    "Qty1 NVARCHAR(2048) NOT NULL, " +
                    "Unit1 NVARCHAR(2048) NOT NULL, " +
                    "Qty2 NVARCHAR(2048) NOT NULL, " +
                    "Unit2 NVARCHAR(2048) NOT NULL, " +
                    "Qty3 NVARCHAR(2048) NOT NULL, " +
                    "Unit3 NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddData(string inputText1, string inputText2, string inputText3, string inputText4, string inputText5, string inputText6, string inputText7, string inputText8)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from Discount;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Discount VALUES (@Entry1,@Entry2,@Entry3,@Entry4,@Entry5,@Entry6,@Entry7,@Entry8);";
                insertCommand.Parameters.AddWithValue("@Entry1", inputText1);
                insertCommand.Parameters.AddWithValue("@Entry2", inputText2);
                insertCommand.Parameters.AddWithValue("@Entry3", inputText3);
                insertCommand.Parameters.AddWithValue("@Entry4", inputText4);
                insertCommand.Parameters.AddWithValue("@Entry5", inputText5);
                insertCommand.Parameters.AddWithValue("@Entry6", inputText6);
                insertCommand.Parameters.AddWithValue("@Entry7", inputText7);
                insertCommand.Parameters.AddWithValue("@Entry8", inputText8);
                

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static String GetMMSchema()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT MMSchema from Discount", db);

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

        public static String GetUnit1()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Unit1 from Discount", db);

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

        public static String GetXYSchema()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT XYSchema from Discount", db);

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

        public static String GetQty1()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Qty1 from Discount", db);

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

        public static String GetQty2()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Qty2 from Discount", db);

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

        public static String GetUnit2()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Unit2 from Discount", db);

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

        public static String GetQty3()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Qty3 from Discount", db);

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

        public static String GetUnit3()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteDiscount.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Unit3 from Discount", db);

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
