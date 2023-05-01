using System;
using System.Data;
using System.Data.SqlClient;

namespace UpdateMembersConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "YourConnectionStringHere"; // Replace with your connection string
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create the stored procedure to update the data
                string createProcedureQuery = @"
                CREATE PROCEDURE UpdateMembers
                AS
                BEGIN
                    UPDATE Members
                    SET SSN = REPLACE(SSN, '-', ''), -- Remove '-' characters from SSN
                        FullName = UPPER(FullName) -- Convert FullName to all uppercase
                END";
                using (SqlCommand createProcedureCommand = new SqlCommand(createProcedureQuery, connection))
                {
                    createProcedureCommand.ExecuteNonQuery();
                }

                // Execute the stored procedure to update the data
                using (SqlCommand command = new SqlCommand("UpdateMembers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Data updated successfully.");
            Console.ReadLine();
        }
    }
}