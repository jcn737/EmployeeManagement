using MediatR;
using EmployeeManagement.Application.DTOs;
using System.Collections.Generic;
using System;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Commands
{
    public class CreateEmployeeCommand : IRequest<Employees>
    {
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