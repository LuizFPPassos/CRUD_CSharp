// Basic CRUD operations with SQL Server in C#
// By Luiz Passos
// Lastest update: May 20, 2024

// Simple console interface

namespace CRUD_CSharp
{
    internal class Program
    {
        private static ClientService clientService = new ClientService();

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Initializing database...");
                clientService.InitializeDatabase();
                Console.WriteLine("Database initialized.");
                Console.WriteLine();
            }
            catch (Exception exIO)
            {
                Console.WriteLine(exIO.Message);
                Console.WriteLine("Press any key to exit application.");
                Console.ReadKey();
                return;
            }

            int menu;
            do
            {
                Console.WriteLine("Menu");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Register client");
                Console.WriteLine("2. Update client data");
                Console.WriteLine("3. List clients");
                Console.WriteLine("4. Search client");
                Console.WriteLine("5. Delete client");
                Console.WriteLine();
                Console.WriteLine("Choose an option: ");
                menu = int.Parse(Console.ReadLine());
                switch (menu)
                {
                    case 0: // EXIT
                        {
                            Console.WriteLine("Exit");
                            break;
                        }

                    case 1: // REGISTER CLIENT
                        {
                            Console.WriteLine("Registration");

                            string name = "";
                            string email = "";
                            string phone = "";
                            string address = "";

                            bool validName = false;
                            bool validEmail = false;
                            bool validPhone = false;
                            bool validAddress = false;

                            int registrationMenu;
                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("Registration Menu");
                                Console.WriteLine("0. Exit");
                                Console.WriteLine("1. Name");
                                Console.WriteLine("2. E-mail");
                                Console.WriteLine("3. Phone number");
                                Console.WriteLine("4. Address");
                                Console.WriteLine("5. Confirm registration");
                                Console.WriteLine();
                                Console.WriteLine("Choose an option: ");

                                registrationMenu = int.Parse(Console.ReadLine());
                                Console.WriteLine();

                                switch (registrationMenu)
                                {
                                    case 0:
                                        Console.WriteLine("Exit");
                                        break;

                                    case 1:
                                        Console.WriteLine("Enter the name: ");
                                        name = Console.ReadLine();
                                        validName = clientService.ValidateName(name);
                                        if (validName != true)
                                        {
                                            Console.WriteLine("Invalid name.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Valid name: " + name + ".");
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine("Enter the e-mail: ");
                                        email = Console.ReadLine();
                                        validEmail = clientService.ValidateEmail(email);
                                        if (validEmail != true)
                                        {
                                            Console.WriteLine("Invalid e-mail.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Valid e-mail: " + email + ".");
                                        }
                                        break;

                                    case 3:
                                        Console.WriteLine("Enter the phone number: ");
                                        phone = Console.ReadLine();
                                        validPhone = clientService.ValidatePhone(phone);
                                        if (validPhone != true)
                                        {
                                            Console.WriteLine("Invalid phone number.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Valid phone number: " + phone + ".");
                                        }
                                        break;

                                    case 4:
                                        Console.WriteLine("Enter the address: ");
                                        address = Console.ReadLine();
                                        validAddress = clientService.ValidateAddress(address);
                                        if (validAddress != true)
                                        {
                                            Console.WriteLine("Invalid address.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Valid address: " + address + ".");
                                        }
                                        break;

                                    case 5:
                                        if (validName && validEmail && validPhone && validAddress)
                                        {
                                            Client client = new Client();
                                            client.Name = name;
                                            client.Email = email;
                                            client.Phone = phone;
                                            client.Address = address;
                                            try
                                            {
                                                clientService.RegisterClient(client);
                                                Console.WriteLine("Client registered.");
                                                break;
                                            }
                                            catch (IOException ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                            catch (ArgumentException exArg)
                                            {
                                                Console.WriteLine(exArg.Message);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid registration. Please enter valid data for all required fields.");
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("Invalid option");
                                        break;
                                } // end switch

                            } while (registrationMenu != 0);

                            Console.WriteLine();
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey();
                            Console.WriteLine();
                            break;
                        }

                    case 2: // UPDATE CLIENT
                        {
                            Console.WriteLine("Update client");
                            Console.WriteLine();

                            if (PrintClients() != true) // Print all clients first
                            {
                                Console.WriteLine("Press any key to return to the main menu.");
                                Console.ReadKey();
                                Console.WriteLine();
                                break;
                            }

                            Console.WriteLine("Enter the id of the client to update: ");
                            try
                            {
                                int id = Convert.ToInt32(Console.ReadLine());

                                if (id < 1)
                                {
                                    Console.WriteLine("Id must be a number greater than 0.");
                                    Console.WriteLine("Press any key to return to the main menu.");
                                    Console.ReadKey();
                                    Console.WriteLine();
                                    break;
                                }
                                else
                                {
                                    try
                                    {
                                        Client client = clientService.ListClientById(id); // List that client

                                        string name = client.Name;
                                        string email = client.Email;
                                        string phone = client.Phone;
                                        string address = client.Address;

                                        bool validName = true;
                                        bool validEmail = true;
                                        bool validPhone = true;
                                        bool validAddress = true;

                                        int updateMenu;
                                        do
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Update Menu");
                                            Console.WriteLine("0. Exit");
                                            Console.WriteLine("1. Name");
                                            Console.WriteLine("2. E-mail");
                                            Console.WriteLine("3. Phone number");
                                            Console.WriteLine("4. Address");
                                            Console.WriteLine("5. Confirm changes");
                                            Console.WriteLine();

                                            Console.WriteLine("Choose an option: ");
                                            updateMenu = int.Parse(Console.ReadLine());

                                            Console.WriteLine();


                                            switch (updateMenu)
                                            {
                                                case 0:
                                                    Console.WriteLine("Exit");
                                                    break;

                                                case 1:
                                                    Console.WriteLine("Enter the name: ");
                                                    name = Console.ReadLine();
                                                    validName = clientService.ValidateName(name);
                                                    if (validName != true)
                                                    {
                                                        Console.WriteLine("Invalid name.");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Valid name: " + name + ".");
                                                    }
                                                    break;

                                                case 2:
                                                    Console.WriteLine("Enter the e-mail: ");
                                                    email = Console.ReadLine();
                                                    validEmail = clientService.ValidateEmail(email);
                                                    if (validEmail != true)
                                                    {
                                                        Console.WriteLine("Invalid e-mail.");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Valid e-mail: " + email + ".");
                                                    }
                                                    break;

                                                case 3:
                                                    Console.WriteLine("Enter the phone number: ");
                                                    phone = Console.ReadLine();
                                                    validPhone = clientService.ValidatePhone(phone);
                                                    if (validPhone != true)
                                                    {
                                                        Console.WriteLine("Invalid phone number.");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Valid phone number: " + phone + ".");
                                                    }
                                                    break;

                                                case 4:
                                                    Console.WriteLine("Enter the address: ");
                                                    address = Console.ReadLine();
                                                    validAddress = clientService.ValidateAddress(address);
                                                    if (validAddress != true)
                                                    {
                                                        Console.WriteLine("Invalid address.");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Valid address: " + address + ".");
                                                    }
                                                    break;

                                                case 5:
                                                    if (validName && validEmail && validPhone && validAddress)
                                                    {
                                                        client.Name = name;
                                                        client.Email = email;
                                                        client.Phone = phone;
                                                        client.Address = address;

                                                        try
                                                        {
                                                            clientService.UpdateClient(client);
                                                            Console.WriteLine("Client updated.");
                                                            break;
                                                        }
                                                        catch (IOException exIO)
                                                        {
                                                            Console.WriteLine("Error: " + exIO.Message);
                                                        }
                                                        catch (ArgumentException exArg)
                                                        {
                                                            Console.WriteLine("Error: " + exArg.Message);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid registration. Please enter valid data for all required fields.");
                                                    }
                                                    break;

                                                default:
                                                    Console.WriteLine("Invalid option");
                                                    Console.WriteLine();
                                                    break;
                                            } // end switch

                                        } while (updateMenu != 0);
                                    }
                                    catch (IOException exIO)
                                    {
                                        Console.WriteLine(exIO.Message);
                                    }
                                    catch (ArgumentException exArg)
                                    {
                                        Console.WriteLine(exArg.Message);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }


                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Id must be a number.");
                            }
                            Console.WriteLine();
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey();
                            Console.WriteLine();
                            break;
                        }

                    case 3: // LIST CLIENTS
                        {
                            Console.WriteLine("List clients");
                            Console.WriteLine();
                            PrintClients();
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey();
                            Console.WriteLine();
                            break;
                        }

                    case 4: // SEARCH CLIENT
                        {
                            Console.WriteLine("Search");
                            Console.WriteLine();
                            Console.WriteLine("Enter the id of the client to search: ");
                            try
                            {
                                int id = Convert.ToInt32(Console.ReadLine());
                                if (id < 1)
                                {
                                    Console.WriteLine("Id must be a number greater than 0.");
                                    Console.WriteLine("Press any key to return to the main menu.");
                                    Console.ReadKey();
                                    Console.WriteLine();
                                    break;
                                }
                                else
                                {
                                    try
                                    {
                                        Client client = clientService.ListClientById(id);
                                        Console.WriteLine();
                                        Console.WriteLine("Client:");
                                        Console.WriteLine("Id: " + client.Id);
                                        Console.WriteLine("Name: " + client.Name);
                                        Console.WriteLine("E-mail: " + client.Email);
                                        Console.WriteLine("Phone: " + client.Phone);
                                        Console.WriteLine("Address: " + client.Address);
                                    }
                                    catch (IOException exIO)
                                    {
                                        Console.WriteLine(exIO.Message);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Id must be a number.");
                            }
                            Console.WriteLine();
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey();
                            Console.WriteLine();
                            break;
                        }

                    case 5: // DELETE CLIENT
                        {
                            Console.WriteLine("Delete");
                            Console.WriteLine();

                            Console.WriteLine("Enter the id of the client to delete: ");

                            try
                            {
                                int id = Convert.ToInt32(Console.ReadLine());

                                if (id < 1)
                                {
                                    Console.WriteLine("Id must be a number greater than 0.");
                                    Console.WriteLine("Press any key to return to the main menu.");
                                    Console.ReadKey();
                                    Console.WriteLine();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Are you sure you want to delete the client with id " + id + "? (1 = yes; 2 = no)");
                                    try
                                    {
                                        int answer = Convert.ToInt32(Console.ReadLine());
                                        if (answer == 1)
                                        {
                                            clientService.DeleteClient(id);
                                            Console.WriteLine("Client deleted.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Deletion canceled.");
                                            Console.WriteLine();
                                        }
                                    }
                                    catch (FormatException exNotInt)
                                    {
                                        Console.WriteLine($"Invalid option: " + exNotInt.Message);
                                    }
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Id must be a number.");
                            }

                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey();
                            Console.WriteLine();
                            break;
                        }

                    default: // DEFAULT
                        {
                            Console.WriteLine("Invalid option");
                            Console.WriteLine();
                            break;
                        }

                } // end switch

            } while (menu != 0);

            Console.WriteLine("Press any key to exit application.");
            Console.ReadKey();
        }

        private static bool PrintClients() // DEDICATED PRINT CLIENTS METHOD
        {
            try
            {
                List<Client> clients = clientService.ReadClients();
                Console.WriteLine("Clients:");
                foreach (Client c in clients)
                {
                    Console.WriteLine("Id: " + c.Id);
                    Console.WriteLine("Name: " + c.Name);
                    Console.WriteLine("E-mail: " + c.Email);
                    Console.WriteLine("Phone: " + c.Phone);
                    Console.WriteLine("Address: " + c.Address);
                    Console.WriteLine();
                }
                return true;
            }
            catch (IOException exIO)
            {
                Console.WriteLine(exIO.Message);
                return false;
            }
            catch (Exception exNotFound)
            {
                Console.WriteLine(exNotFound.Message);
                return false;
            }
        }
    }
}

