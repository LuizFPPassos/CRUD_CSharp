// Basic CRUD operations with SQL Server in C#
// By Luiz Passos
// Lastest update: May 20, 2024

// Service class that executes validations, calls for DAO methods, and throws exceptions messages where applicable

using System.Text.RegularExpressions;

namespace CRUD_CSharp
{
    internal class ClientService 
    {
        private ClientDAO clientDAO;
        public ClientService()
        {
            this.clientDAO = new ClientDAO();
        }

        public void InitializeDatabase()
        {
            try
            {
                clientDAO.InitializeDatabase();
            }
            catch (Exception exIO)
            {
                throw new IOException("Error initializing database: " + exIO.Message);
            }
        }

        public bool ValidateName(string name)
        {
            if (name == null || name.Length < 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidatePhone(string phone)
        {
            if (phone == null || phone.Length < 7)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateAddress(string address)
        {
            if (address == null || address.Length < 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RegisterClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentException("Invalid client.");
            }
            else
            {
                try
                {
                    clientDAO.RegisterClient(client);
                }
                catch (Exception exIO)
                {
                    throw new Exception("Error registering client: " + exIO.Message);
                }
            }
        }

        public void UpdateClient(Client client)
        {
            if (client == null)
            {
                Console.WriteLine("Invalid client.");
            }
            else
            {
                try
                {
                    clientDAO.UpdateClient(client);
                }
                catch (Exception exIO)
                {
                    throw new Exception("Error updating client: " + exIO.Message);
                }
            }
        }

        public List<Client> ReadClients()
        {
            try
            {
                List<Client> clients = clientDAO.ReadClients();
                if (clients.Count == 0)
                {
                    throw new Exception("No clients found.");
                }
                else
                {
                    return clients;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading clients: " + ex.Message);
            }
        }

        public Client ListClientById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id must be a number greater than 0.");
            }
            else
            {
                try
                {
                    Client client = clientDAO.ListClientById(id);
                    if (client == null)
                    {
                        throw new Exception("Client not found.");
                    }
                    else
                    {
                        return client;
                    }
                }
                catch (Exception exIO)
                {
                    throw new IOException("Error: " + exIO.Message);
                }
            }
        }

        public void DeleteClient(int id)
        {
            if (id < 1)
            {
                Console.WriteLine("Invalid ID.");
            }
            else
            {
                try
                {
                    clientDAO.DeleteClient(id);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting client: " + ex.Message);
                }
                
            }
        }

    }
}
