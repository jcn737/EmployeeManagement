using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities
{
    public class Employees
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public List<string> Phones { get; set; } = new List<string>();
        public Guid? ManagerId { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }

        public bool IsOfLegalAge() => (DateTime.Today.Year - BirthDate.Year) >= 18;
    }
}
