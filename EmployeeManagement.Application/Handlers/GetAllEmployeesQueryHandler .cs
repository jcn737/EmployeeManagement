using EmployeeManagement.Application.Queries;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Handlers
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employees>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllEmployeesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employees>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.ToListAsync(cancellationToken);
        }
    }
}
