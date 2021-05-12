using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteData
{
    public static class PurchaseOrderFilterType
    {

        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePurchaseType.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Purchasetype (Type NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddPurchaseType(string inputText1)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePurchaseType.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from Purchasetype;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePurchaseType.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Purchasetype VALUES (@Entry1);";
                insertCommand.Parameters.AddWithValue("@Entry1", inputText1);


                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static String GetPurchaseType()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqlitePurchaseType.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Type from Purchasetype", db);

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
