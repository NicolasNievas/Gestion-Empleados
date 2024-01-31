import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChargeDTO, EmployeeDTO, SucursalDTO } from '../Models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class RestService {

  constructor(private httpClient: HttpClient) { }

  private apiGetUrl = 'https://localhost:7289/api/Gets';
  private apiCommandUrl = 'https://localhost:7289/api/Commands';

  public createEmpleado(empleado: EmployeeDTO): Observable<any> {
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
}
