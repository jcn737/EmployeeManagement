using System;
using System.Collections.Generic;

namespace EmployeeManagement.Application.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public List<string> Telefones { get; set; }
        public Guid? GestorId { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}