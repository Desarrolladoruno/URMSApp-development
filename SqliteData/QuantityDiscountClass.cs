using System;
using Microsoft.Data.Sqlite;

namespace SqliteData
{
    public static class QuantityDiscountClass
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteQuantityDiscounts.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS QuantityDiscount (Description NVARCHAR(2048) NOT NULL, " +
                    "ID NVARCHAR(2048) NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddData(string inputText1, string inputText2)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteQuantityDiscounts.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "Delete from QuantityDiscount;";

                insertCommand.ExecuteReader();

                db.Close();
            }

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteQuantityDiscounts.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO QuantityDiscount VALUES (@Entry1,@Entry2);";                    ;
                insertCommand.Parameters.AddWithValue("@Entry1", inputText1);
                insertCommand.Parameters.AddWithValue("@Entry2", inputText2);
                                
                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static String GetQuantityId()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteQuantityDiscounts.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT ID from QuantityDiscount", db);

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

        public static String GetQuantityDescription()
        {
            String bc = "";

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteQuantityDiscounts.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Description from QuantityDiscount", db);

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
