import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from './employee/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private baseUrl = 'https://api.exemplo.com/employees'; // Ajuste para o endpoint correto

  constructor(private http: HttpClient) {}

  // Obter todos os funcionários
  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.baseUrl);
  }

  // Obter funcionário por ID
  getEmployee(id: string): Observable<Employee> {
    return this.http.get<Employee>(`${this.baseUrl}/${id}`);
  }

  // Criar um funcionário
  createEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.baseUrl, employee);
  }

  // Atualizar um funcionário
  updateEmployee(id: string, employee: Employee): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, employee);
  }

  // Excluir um funcionário
  deleteEmployee(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
