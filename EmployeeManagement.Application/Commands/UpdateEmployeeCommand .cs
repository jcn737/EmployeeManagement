using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands
{
    public class UpdateEmployeeCommand : IRequest<Employees>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public List<string> Phones { get; set; }
        public Guid? ManagerId { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
