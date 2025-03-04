using EmployeeManagement.API.Repository.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employees>> GetAllAsync()
        {
            try
            {
                var employees = await _context.Employees.ToListAsync();
                return employees;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while fetching all employees.");
                throw;
            }
        }

        public async Task<Employees> GetByIdAsync(Guid id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    Logger.Warn($"Employee with ID {id} not found.");
                    return null;
                }

                return employee;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while fetching employee by ID.");
                throw;
            }
        }

        public async Task AddAsync(Employees employees)
        {
            try
            {
                _context.Employees.Add(employees);
                await _context.SaveChangesAsync();
                Logger.Info($"Employee {employees.FirstName} {employees.LastName}  added successfully with ID: {employees.Id}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while adding employee.");
                throw;
            }
        }

        public async Task UpdateAsync(Employees employees)
        {
            try
            {
                _context.Employees.Update(employees);
                await _context.SaveChangesAsync();
                Logger.Info($"Employee with ID {employees.Id} updated successfully.");
            }
            catch (DbUpdateException ex)
            {
                Logger.Error(ex, "Error occurred while updating employee.");
                throw new InvalidOperationException("Database update failed.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var employees = await _context.Employees.FindAsync(id);

                if (employees == null)
                {
                    Logger.Warn($"Employee with ID {id} not found for deletion.");
                    return;
                }

                _context.Employees.Remove(employees);
                await _context.SaveChangesAsync();
                Logger.Info($"Employee with ID {id} deleted successfully.");
            }
            catch (DbUpdateException ex)
            {
                Logger.Error(ex, "Error occurred while deleting employee.");
                throw new InvalidOperationException("Database update failed.", ex);
            }
        }
    }
}
