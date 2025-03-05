// app.component.ts
import { Component } from '@angular/core';

// Definindo a interface Employee
interface Employee {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  document: string;
  phones: string[];
  managerId?: string;
  password: string;
  birthDate: string; // ISO 8601
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  // Lista de funcionários
  employees: Employee[] = [
    {
      id: '1',
      firstName: 'João',
      lastName: 'Silva',
      email: 'joao.silva@email.com',
      document: '123456789',
      phones: ['(11) 99999-9999'],
      managerId: '1',
      password: 'senha123',
      birthDate: '1980-01-01T00:00:00Z'
    },
    {
      id: '2',
      firstName: 'Ana',
      lastName: 'Souza',
      email: 'ana.souza@email.com',
      document: '987654321',
      phones: ['(11) 98888-8888'],
      managerId: '2',
      password: 'senha456',
      birthDate: '1985-02-02T00:00:00Z'
    }
  ];

  // Novo funcionário
  newEmployee: Employee = {
    id: '',
    firstName: '',
    lastName: '',
    email: '',
    document: '',
    phones: [],
    managerId: '',
    password: '',
    birthDate: ''
  };

  // Função para adicionar um novo funcionário
  onSubmit() {
    const newEmployeeId = (this.employees.length + 1).toString();
    const newEmployeeToAdd: Employee = {
      ...this.newEmployee,
      id: newEmployeeId
    };
    this.employees.push(newEmployeeToAdd);
    this.resetForm();
  }

  // Função para resetar o formulário após o envio
  resetForm() {
    this.newEmployee = {
      id: '',
      firstName: '',
      lastName: '',
      email: '',
      document: '',
      phones: [],
      managerId: '',
      password: '',
      birthDate: ''
    };
  }
}
