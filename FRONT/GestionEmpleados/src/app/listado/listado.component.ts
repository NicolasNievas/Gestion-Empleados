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
        alert('Error al obtener la lista de empleados');
        console.error('Error al obtener la lista de empleados', error);
      }
    );
  }
  confirmarEliminacion(empleadoId: number): void {
    const confirmacion = confirm('¿Estás seguro de que deseas eliminar este empleado?');
    if (confirmacion) {
      this.eliminarEmpleado(empleadoId);
    }
  }
  eliminarEmpleado(empleadoId: number): void {
    this.rest.DeleteEmployee(empleadoId).subscribe(
      () => {
        alert('Empleado eliminado con éxito');
        this.obtenerEmpleados(); // Vuelve a cargar la lista después de la eliminación
      },
      (error) => {
        alert('Error al eliminar el empleado');
        console.error('Error al eliminar el empleado', error);
      }
    );
  }
  editarEmpleado(empleadoId: number): void {
    alert('Funcionalidad no implementada');
  }
}
