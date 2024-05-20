// Basic CRUD operations with SQL Server in C#
// By Luiz Passos
// Lastest update: May 20, 2024

// Class that represents the Client entity

namespace CRUD_CSharp
{
    internal class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
