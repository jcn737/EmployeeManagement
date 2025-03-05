export interface Employee {
  id: string; // Guid
  firstName: string;
  lastName: string;
  email: string;
  document: string;
  phones: string[]; // Lista de telefones
  managerId?: string; // Pode ser nulo
  password: string;
  birthDate: string; // DateTime no formato ISO 8601
}
