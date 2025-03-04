using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Repository.Interfaces
{
    public interface IJWTService
    {
        string GenerateToken(string username);
        Task<ClaimsPrincipal> ValidateTokenAsync(string token);
    }
}
