using EmployeeManagement.API.Repository.Interfaces;
using EmployeeManagement.Application.Commands;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Handlers
{
    public class GenerateTokenHandler : IRequestHandler<GenerateTokenRequest, string>
    {
        private readonly IJWTService _jwtService;

        public GenerateTokenHandler(IJWTService jwtService)
        {
            _jwtService = jwtService;
        }

        public Task<string> Handle(GenerateTokenRequest request, CancellationToken cancellationToken)
        {
            var token = _jwtService.GenerateToken(request.Username);
            return Task.FromResult(token);
        }
    }
}
