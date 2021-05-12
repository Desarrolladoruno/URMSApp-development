using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SqliteData
{
    public static class PurchaseClass
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePurchaseO.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Purchase (Purchase_Id NVARCHAR(2048) NOT NULL, " +
                    "Purchase_Number NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddPurchase(string inputText1, string inputText2)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePurchaseO.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from Purchase;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePurchaseO.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Purchase VALUES (@Entry1,@Entry2);";
                insertCommand.Parameters.AddWithValue("@Entry1", inputText1);
                insertCommand.Parameters.AddWithValue("@Entry2", inputText2);


                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static String GetPurchaseId()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePurchaseO.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Purchase_Id from Purchase", db);

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

        public static String GetPurchasePONumber()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePurchaseO.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Purchase_Number from Purchase", db);

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
