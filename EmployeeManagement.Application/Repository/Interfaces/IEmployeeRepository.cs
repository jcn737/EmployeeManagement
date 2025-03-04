using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employees>> GetAllAsync();
        Task<Employees> GetByIdAsync(Guid id);
        Task AddAsync(Employees funcionario);
        Task UpdateAsync(Employees funcionario);
        Task DeleteAsync(Guid id);
    }
}
