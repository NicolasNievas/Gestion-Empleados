import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChargeDTO, Employee, EmployeeDTO, SucursalDTO } from '../Models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class RestService {

  constructor(private httpClient: HttpClient) { }

  private apiGetUrl = 'https://localhost:7289/api/Gets';
  private apiCommandUrl = 'https://localhost:7289/api/Commands';
  private apiUserUrl = 'https://localhost:7289/api/User';

  public createEmpleado(empleado: Employee): Observable<any> {
    return this.httpClient.post(`${this.apiCommandUrl}/PostEmployee`, empleado);
  }

  public GetEmployees(): Observable<EmployeeDTO[]> {
    return this.httpClient.get<EmployeeDTO[]>(`${this.apiGetUrl}/Employees`);
  }
  
  public GetCharges(): Observable<ChargeDTO[]>{
    return this.httpClient.get<ChargeDTO[]>(`${this.apiGetUrl}/Charges`);
  }

  public GetSucursales(): Observable<SucursalDTO[]>{
    return this.httpClient.get<SucursalDTO[]>(`${this.apiGetUrl}/Sucursales`);
  }

  public DeleteEmployee(empleadoId: number): Observable<any> {
    return this.httpClient.delete(`${this.apiCommandUrl}/DeleteEmployee/${empleadoId}`);
  }

  public signUp(user: any): Observable<any> {
    return this.httpClient.post(`${this.apiUserUrl}/Sing_Up`, user);
  }  

  public login(credentials: any): Observable<any> {
    return this.httpClient.post(`${this.apiUserUrl}/Login`, credentials);
  }
}
