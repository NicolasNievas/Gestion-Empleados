import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RestService } from '../Services/rest.service';
import {
  Employee,
  ChargeDTO,
  SucursalDTO,
  EmployeeDTO,
} from '../Models/employee.model';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
})
export class CreateComponent implements OnInit {
  empleadoForm: FormGroup = this.fb.group({});
  cargos: ChargeDTO[] = [];
  jefes: EmployeeDTO[] = [];
  sucursales: SucursalDTO[] = [];

  constructor(
    private fb: FormBuilder,
    private rest: RestService,
    private router: Router,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.empleadoForm = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(3)]],
      apellido: ['', [Validators.required]],
      dni: ['', [Validators.required]],
      cargoId: ['', [Validators.required]],
      sucursalId: ['', [Validators.required]],
      ciudad: [{ value: '', disabled: true }],
      jefeId: [''],
      hora: [{ value: '', disabled: true }],
    });
    this.rest.GetCharges().subscribe((cargos) => {
      this.cargos = cargos;
    });

    this.rest.GetEmployees().subscribe((jefes) => {
      this.jefes = jefes;
    });

    this.rest.GetSucursales().subscribe((sucursales) => {
      this.sucursales = sucursales;
    });
  }

  onSucursalChange(): void {
    const selectedSucursal = this.sucursales.find(
      (s) => s.id === +this.empleadoForm.get('sucursalId')?.value
    );
    if (selectedSucursal) {
      this.empleadoForm.get('ciudad')?.setValue(selectedSucursal.city.name);
    }
  }

  onSubmit(): void {
    if (this.empleadoForm.valid) {
      // Obtener la hora actual y formatearla como 'HH:mm'
      const formattedTime = this.datePipe.transform(new Date(), 'HH:mm') || '--:--';
  
      // Asignar la hora formateada al nuevo empleado
      console.log('Cargo ID:', this.empleadoForm.get('cargoId')?.value);
      console.log('Sucursal ID:', this.empleadoForm.get('sucursalId')?.value);
      console.log('Jefe ID:', this.empleadoForm.get('jefeId')?.value);
  
      // Asignar los valores al nuevo empleado
      const nuevoEmpleado: Employee = {
        id: 0,
        name: this.empleadoForm.get('nombre')?.value,
        lastName: this.empleadoForm.get('apellido')?.value,
        charge: { id: +this.empleadoForm.get('cargoId')?.value, name: '' },
        sucursal: { id: +this.empleadoForm.get('sucursalId')?.value, name: '', city: { id: 0, name: '' } },
        dni: this.empleadoForm.get('dni')?.value,
        fechaAlta: new Date(),
        jefe: null,
        employees: []
      };
  
      console.log('Empleado a enviar:', nuevoEmpleado);
  
      this.rest.createEmpleado(nuevoEmpleado).subscribe(
        (response) => {
          console.log('Empleado creado con éxito:', response);
          this.router.navigate(['/listado']);
        },
        (error) => {
          console.error('Error al crear empleado:', error);
  
          if (error instanceof HttpErrorResponse && error.error) {
            console.error('Detalles del error de validación:', error.error);
          }
        }
      );
    } else {
      console.warn('Formulario no válido. Revise los campos.');
    }
  }
  
  
}
