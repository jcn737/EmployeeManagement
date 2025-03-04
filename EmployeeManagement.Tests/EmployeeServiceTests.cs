using EmployeeManagement.Application.Commands;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.API.Repository.Interfaces;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;

        public EmployeeServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();  // Mock para o Mediator
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();  // Mock para o repositório
        }

        // Utilitário para criar instâncias de Employees
        private Employees CreateMockEmployee(Guid id, string firstName = "John", string lastName = "Doe", string email = "john.doe@example.com", string document = "12345678901")
        {
            return new Employees
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Document = document,
                Phones = new List<string> { "123456789", "987654321" },
                ManagerId = null, // Ou um valor válido
                Password = "Password123",
                BirthDate = new DateTime(1990, 1, 1)
            };
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnEmployee_WhenEmployeeIsCreated()
        {
            // Arrange
            var command = new CreateEmployeeCommand { FirstName = "John", LastName = "Doe" };
            var resultEmployee = CreateMockEmployee(Guid.NewGuid());

            // Configurando o mock do Mediator para retornar o employee simulado
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEmployeeCommand>(), default))
                .ReturnsAsync(resultEmployee);

            // Act
            var createdEmployee = await _mediatorMock.Object.Send(command);

            // Assert
            Assert.NotNull(createdEmployee);
            Assert.Equal(resultEmployee.Id, createdEmployee.Id);
            Assert.Equal("John", createdEmployee.FirstName);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = CreateMockEmployee(employeeId);

            // Configurando o mock do repositório para retornar o employee simulado
            _employeeRepositoryMock.Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync(employee);

            // Act
            var returnedEmployee = await _employeeRepositoryMock.Object.GetByIdAsync(employeeId);

            // Assert
            Assert.NotNull(returnedEmployee);
            Assert.Equal(employeeId, returnedEmployee.Id);
            Assert.Equal("John", returnedEmployee.FirstName);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnNull_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

            // Configurando o mock do repositório para retornar null
            _employeeRepositoryMock.Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync((Employees)null);

            // Act
            var returnedEmployee = await _employeeRepositoryMock.Object.GetByIdAsync(employeeId);

            // Assert
            Assert.Null(returnedEmployee);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnSuccess_WhenEmployeeIsDeleted()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

            // Configurando o mock para simular sucesso ao excluir o funcionário
            _employeeRepositoryMock.Setup(r => r.DeleteAsync(employeeId))
                .Returns(Task.CompletedTask);

            // Act
            await _employeeRepositoryMock.Object.DeleteAsync(employeeId);

            // Assert - Verificando se o método DeleteAsync foi chamado corretamente
            _employeeRepositoryMock.Verify(r => r.DeleteAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldThrowException_WhenDatabaseErrorOccurs()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

            // Configurando o mock para lançar uma exceção
            _employeeRepositoryMock.Setup(r => r.DeleteAsync(employeeId))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _employeeRepositoryMock.Object.DeleteAsync(employeeId));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
