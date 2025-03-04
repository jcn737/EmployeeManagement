using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands
{
    public class ValidateTokenRequest : IRequest<ClaimsPrincipal>
    {
        public string Token { get; set; }
    }
}
