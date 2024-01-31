import { Component, OnInit } from '@angular/core';
import { EmployeeDTO } from '../Models/employee.model';
import { RestService } from '../Services/rest.service';

@Component({
  selector: 'app-listado',
  templateUrl: './listado.component.html',
  styleUrls: ['./listado.component.css']
})
export class ListadoComponent implements OnInit{
  empleados: EmployeeDTO[] = [];

  constructor(private rest: RestService) { }

  ngOnInit(): void {
    this.obtenerEmpleados();
  }

  obtenerEmpleados() {
    this.rest.GetEmployees().subscribe(
      (data) => {
        this.empleados = data;
      },
      (error) => {
        console.error('Error al obtener la lista de empleados', error);
      }
    );
  }
}
