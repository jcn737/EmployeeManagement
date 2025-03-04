using EmployeeManagement.Application.Queries;
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
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employees>
    {
        private readonly ApplicationDbContext _context;

        public GetEmployeeByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employees> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.FindAsync(request.Id);
        }
    }
}
