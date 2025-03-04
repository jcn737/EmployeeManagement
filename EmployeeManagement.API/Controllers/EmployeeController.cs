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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(); // Inicializando o logger

        public EmployeeController(IMediator mediator, IEmployeeRepository employeeRepository)
        {
            _mediator = mediator;
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
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
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            try
            {
                Logger.Info("Fetching all employees");

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
        public async Task<ActionResult<Employees>> GetEmployee(int id)
        {
            try
            {
                var funcionario = await _employeeRepository.GetByIdAsync(id);

                if (funcionario == null)
                {
                    Logger.Warn($"Employee with ID {id} not found");
                    return NotFound();
                }

                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while fetching employee by ID");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employees>> PostEmployee(Employees employees)
        {
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
            try
            {
                if (id != employees.Id)
                {
                    Logger.Warn($"ID mismatch: provided ID {id} does not match employee ID {employees.Id}");
                    return BadRequest("ID do funcionário não corresponde.");
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
        public async Task<IActionResult> DeleteEmployee(int id)
        {
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
