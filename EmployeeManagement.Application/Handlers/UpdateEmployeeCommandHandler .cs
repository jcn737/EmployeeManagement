using EmployeeManagement.Application.Commands;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Handlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employees>
    {
        private readonly ApplicationDbContext _context;
        public UpdateEmployeeCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employees> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var funcionario = await _context.Employees.FindAsync(request.Id);
            if (funcionario == null)
            {
                return null;
            }

            funcionario.LastName = request.LastName;
            funcionario.Email = request.Email;
            funcionario.Document = request.Document;
            funcionario.Phones = request.Phones;
            funcionario.ManagerId = request.ManagerId;
            funcionario.Password = request.Password;
            funcionario.BirthDate = request.BirthDate;


            await _context.SaveChangesAsync(cancellationToken);

            return funcionario;
        }

    }
}
