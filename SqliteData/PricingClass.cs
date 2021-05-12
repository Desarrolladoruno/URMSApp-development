using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SqliteData
{
    public static class PricingClass
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Pricing (PriceA NVARCHAR(2048) NOT NULL, " +
                    "PriceB NVARCHAR(2048) NOT NULL, " +
                    "PriceC NVARCHAR(2048) NOT NULL, " +
                    "SalePrice NVARCHAR(2048) NOT NULL, " +
                    "Star NVARCHAR(2048) NOT NULL, " +
                    "End NVARCHAR(2048) NOT NULL, " +
                    "Schedule NVARCHAR(2048) NOT NULL, " +
                    "SaleType NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddData(string inputText1, string inputText2, string inputText3, string inputText4, string inputText5, string inputText6, string inputText7, string inputText8)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from Pricing;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Pricing VALUES (@Entry1,@Entry2,@Entry3,@Entry4,@Entry5,@Entry6,@Entry7,@Entry8);";
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

        public static String GetPriceA()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT PriceA from Pricing", db);

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


        public static String GetPriceB()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT PriceB from Pricing", db);

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

        public static String GetPriceC()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT PriceC from Pricing", db);

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

        public static String GetSalePrice()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT SalePrice from Pricing", db);

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

        public static String GetStar()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Star from Pricing", db);

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

        public static String GetEnd()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT End from Pricing", db);

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

        public static String GetSchedule()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Schedule from Pricing", db);

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

        public static String GetSaleType()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePricing.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT SaleType from Pricing", db);

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
