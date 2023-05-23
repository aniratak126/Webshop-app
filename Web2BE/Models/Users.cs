using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2BE.Models
{
    public class Users
    {
        private int id;
        private string firstName;
        private string lastName;
        private string password;
        private string email;
        private decimal euro;
        private string type;
        private int status;
        private DateTime createdOn;

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public decimal Euro { get => euro; set => euro = value; }
        public string Type { get => type; set => type = value; }
        public int Status { get => status; set => status = value; }
        public DateTime CreatedOn { get => createdOn; set => createdOn = value; }
    }
}
