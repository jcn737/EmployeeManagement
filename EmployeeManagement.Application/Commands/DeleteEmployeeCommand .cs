using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteEmployeeCommand(Guid id)
        {
            Id = id;
        }
    }
}
