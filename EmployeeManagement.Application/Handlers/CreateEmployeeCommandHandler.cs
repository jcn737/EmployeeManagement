using EmployeeManagement.Application.Commands;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employees>
    {
        private readonly ApplicationDbContext _context;

        public CreateEmployeeCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employees> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employees = new Employees
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Document = request.Document,
                Phones = request.Phones ?? new List<string>(),
                ManagerId = request.ManagerId,
                Password = request.Password,
                BirthDate = request.BirthDate,
            };


            _context.Employees.Add(employees);
            await _context.SaveChangesAsync(cancellationToken);

            return employees;
        }
    }
}
