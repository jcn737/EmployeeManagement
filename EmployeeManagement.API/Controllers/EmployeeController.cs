using EmployeeManagement.API.Repository.Interfaces;
using EmployeeManagement.Application.Commands;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public EmployeeController(IMediator mediator, IEmployeeRepository employeeRepository)
        {
            _mediator = mediator;
            _employeeRepository = employeeRepository;
        }

        private async Task<IActionResult> ValidateToken()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var token))
            {
                return Unauthorized("Token is missing.");
            }

            var principal = await _mediator.Send(new ValidateTokenRequest { Token = token.ToString().Replace("Bearer ", "") });
            if (principal == null)
            {
                return Unauthorized("Invalid token.");
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var authResult = await ValidateToken();
            if (authResult != null) return authResult;

            try
            {
                var result = await _mediator.Send(command);
                Logger.Info($"Employee created successfully with ID: {result.Id}");
                return CreatedAtAction(nameof(CreateEmployee), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while creating employee");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var authResult = await ValidateToken();
            if (authResult != null) return authResult;

            try
            {
                var employees = await _employeeRepository.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while fetching employees");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var authResult = await ValidateToken();
            if (authResult != null) return authResult;

            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    Logger.Warn($"Employee with ID {id} not found");
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while fetching employee by ID");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> PostEmployee(Employees employees)
        {
            var authResult = await ValidateToken();
            if (authResult != null) return authResult;

            try
            {
                await _employeeRepository.AddAsync(employees);
                Logger.Info($"Employee {employees.FirstName} {employees.LastName} added successfully");
                return CreatedAtAction(nameof(GetEmployee), new { id = employees.Id }, employees);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while adding employee");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employees employees)
        {
            var authResult = await ValidateToken();
            if (authResult != null) return authResult;

            try
            {
                if (id != employees.Id)
                {
                    Logger.Warn($"ID mismatch: provided ID {id} does not match employee ID {employees.Id}");
                    return BadRequest("ID do funcion�rio n�o corresponde.");
                }

                await _employeeRepository.UpdateAsync(employees);
                Logger.Info($"Employee with ID {id} updated successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while updating employee");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var authResult = await ValidateToken();
            if (authResult != null) return authResult;

            try
            {
                await _employeeRepository.DeleteAsync(id);
                Logger.Info($"Employee with ID {id} deleted successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while deleting employee");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
