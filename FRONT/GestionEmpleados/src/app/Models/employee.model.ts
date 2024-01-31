export interface ChargeDTO {
    id: number;
    name: string;
  }
  
  export interface CityDTO {
    id: number;
    name: string;
  }
  
  export interface UserDTO {
    id: number;
    name: string;
    password: string;
  }
  
  export interface SucursalDTO {
    id: number;
    name: string;
    city: CityDTO;
  }
  
  export interface EmployeeDTO {
    id: number;
    name: string;
    lastName: string;
    charge: ChargeDTO;
    sucursal: SucursalDTO;
    dni: string;
    jefe?: EmployeeDTO | null;
  }

  // employee.model.ts

  export interface Employee {
    id: number;
    name: string;
    lastName: string;
    charge: Charge;
    sucursal: Sucursal;
    dni: string;
    fechaAlta: Date;
    jefe: Employee | null;  // Permitir explícitamente null
    employees?: Employee[];
  }
  
  
  export interface Charge {
    id: number;
    name: string;
    employees?: Employee[];
  }
  
  export interface Sucursal {
    id: number;
    name: string;
    city: City;
    employees?: Employee[];
  }
  
  export interface City {
    id: number;
    name: string;
    sucursals?: Sucursal[];
  }
  
  export interface User {
    id: number;
    name: string;
    password: string;
  }  
  