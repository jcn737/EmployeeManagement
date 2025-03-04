using EmployeeManagement.Application.Commands;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Handlers
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteEmployeeCommandHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Id do funcionário não pode ser vazio.", nameof(request.Id));
            }

            var funcionario = await _context.Employees.FindAsync(new object[] { request.Id }, cancellationToken);
            if (funcionario == null)
            {
                return false;
            }

            _context.Employees.Remove(funcionario);

            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
