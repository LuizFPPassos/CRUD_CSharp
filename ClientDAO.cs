// Basic CRUD operations with SQL Server in C#
// By Luiz Passos
// Lastest update: May 20, 2024

// DAO class that executes SQL CRUD commands and returns the results to the Service class

using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUD_CSharp
{
    internal class ClientDAO 
    {
        //private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;";
        private string connectionString;
        private string databaseName = "CRUDCSharpDB";

        public ClientDAO()
        {
            this.connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        private void ExecuteSqlCommand(string connectionString, string sqlCommand) // Executes a simple SQL command
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InitializeDatabase() // Creates the database and the table if they don't exist
        {
            string createDatabaseIfNotExists = $@"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}')
            BEGIN
                CREATE DATABASE {databaseName};
            END";

            ExecuteSqlCommand(connectionString, createDatabaseIfNotExists);

            connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database={databaseName};Integrated Security=true;";

            if (!TableExists("client")) // Calls method to verify if the table exists
            {
                // Creates table if it doesn't already exist
                string createTable = @"
                CREATE TABLE client (
                    id_client INT PRIMARY KEY IDENTITY,
                    name_client NVARCHAR(100),
                    email_client NVARCHAR(50) UNIQUE,
                    phone_client NVARCHAR(20),
                    address_client NVARCHAR(255)
                )";

                ExecuteSqlCommand(connectionString, createTable);
            }
        }

        private bool TableExists(string tableName) // Dedicated method to verify if the table exists
        {
            bool exists = false;
            string checkTableExists = $@"
            IF EXISTS (SELECT * FROM sys.tables WHERE name = '{tableName}')
            BEGIN
                SELECT 1;
            END
            ELSE
            BEGIN
                SELECT 0;
            END";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(checkTableExists, connection))
                {
                    exists = (int)command.ExecuteScalar() == 1; // If the table exists, the method returns true
                }
            }
            return exists;
        }

        public void RegisterClient(Client client) // Creates a new client in the database
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertClientQuery = "INSERT INTO client (name_client, email_client, phone_client, address_client) VALUES (@Name, @Email, @Phone, @Address)";
                using (SqlCommand insertClientCommand = new SqlCommand(insertClientQuery, connection))
                {
                    insertClientCommand.Parameters.AddWithValue("@Name", client.Name);
                    insertClientCommand.Parameters.AddWithValue("@Email", client.Email);
                    insertClientCommand.Parameters.AddWithValue("@Phone", client.Phone);
                    insertClientCommand.Parameters.AddWithValue("@Address", client.Address);
                    insertClientCommand.ExecuteNonQuery();
                }
            }
        }

        public void UpdateClient(Client client) // Updates a client's information
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateClientQuery = "UPDATE client SET name_client = @Name, email_client = @Email, phone_client = @Phone, address_client = @Address WHERE id_client = @Id";
                using (SqlCommand updateClientCommand = new SqlCommand(updateClientQuery, connection))
                {
                    updateClientCommand.Parameters.AddWithValue("@Name", client.Name);
                    updateClientCommand.Parameters.AddWithValue("@Email", client.Email);
                    updateClientCommand.Parameters.AddWithValue("@Phone", client.Phone);
                    updateClientCommand.Parameters.AddWithValue("@Address", client.Address);
                    updateClientCommand.Parameters.AddWithValue("@Id", client.Id);
                    updateClientCommand.ExecuteNonQuery();
                }
            }
        }

        public List<Client> ReadClients() // Reads all clients
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM client";
                SqlCommand command = new SqlCommand(query, connection);

                List<Client> clients = new List<Client>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Client client = new Client
                        {
                            Id = reader.GetInt32("id_client"),
                            Name = reader.GetString("name_client"),
                            Email = reader.GetString("email_client"),
                            Phone = reader.GetString("phone_client"),
                            Address = reader.GetString("address_client"),
                        };
                        clients.Add(client);
                    }
                }
                return clients;
            }
        }

        public Client ListClientById(int clientId) // Searches a client by its id
        {
            Client client = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM client WHERE id_client = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", clientId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                {
                    if (reader.Read())
                    {
                        client = new Client
                        {
                            Id = reader.GetInt32("id_client"),
                            Name = reader.GetString("name_client"),
                            Email = reader.GetString("email_client"),
                            Phone = reader.GetString("phone_client"),
                            Address = reader.GetString("address_client"),
                        };
                    }
                    return client;
                }
            }
        }
        public void DeleteClient(int clientId) // Deletes a client
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteClientQuery = "DELETE FROM client WHERE id_client = @Id";
                using (SqlCommand deleteClientCommand = new SqlCommand(deleteClientQuery, connection))
                {
                    deleteClientCommand.Parameters.AddWithValue("@Id", clientId);
                    deleteClientCommand.ExecuteNonQuery();
                }
            }
        }


    }
}
