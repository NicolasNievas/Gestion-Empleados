import { Component, OnInit } from '@angular/core';
import { EmployeeDTO } from '../Models/employee.model';
import { RestService } from '../Services/rest.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-listado',
  templateUrl: './listado.component.html',
  styleUrls: ['./listado.component.css'],
})
export class ListadoComponent implements OnInit {
  empleados: EmployeeDTO[] = [];

  constructor(private rest: RestService) {}

  ngOnInit(): void {
    this.obtenerEmpleados();
  }

  obtenerEmpleados() {
    this.rest.GetEmployees().subscribe(
      (data) => {
        this.empleados = data;
      },
      (error) => {
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Error al obtener la lista de empleados',
          text: 'Hubo un error al intentar obtener la lista de empleados. Intente nuevamente.',
          showConfirmButton: false,
          timer: 2000,
        });
        console.error('Error al obtener la lista de empleados', error);
      }
    );
  }
  confirmarEliminacion(empleadoId: number): void {
    Swal.fire({
      title: `¿Estás seguro de eliminar a '${
        this.empleados.find((e) => e.id === empleadoId)?.name
      } ${this.empleados.find((e) => e.id === empleadoId)?.lastName}'?`,
      text: 'Esta acción no se puede deshacer.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminar',
    }).then((result) => {
      if (result.isConfirmed) {
        this.eliminarEmpleado(empleadoId);
      }
    });
  }
  eliminarEmpleado(empleadoId: number): void {
    this.rest.DeleteEmployee(empleadoId).subscribe(
      () => {
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'Empleado eliminado con éxito',
          text: 'El empleado se ha eliminado correctamente.',
          showConfirmButton: false,
          timer: 1500,
        });
        this.obtenerEmpleados();
      },
      (error) => {
        Swal.fire({
          position: 'center',
          icon: 'error',
          title: 'Error al eliminar el empleado',
          text: 'Hubo un error al intentar eliminar el empleado. Intente nuevamente.',
          showConfirmButton: false,
          timer: 2000,
        });
        console.error('Error al eliminar el empleado', error);
      }
    );
  }
  editarEmpleado(empleadoId: number): void {
    alert('Funcionalidad no implementada');
  }
}
