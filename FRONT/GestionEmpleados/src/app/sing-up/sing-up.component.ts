import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RestService } from '../Services/rest.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-sing-up',
  templateUrl: './sing-up.component.html',
  styleUrls: ['./sing-up.component.css'],
})
export class SingUpComponent implements OnInit {
  Form: FormGroup = this.fb.group({});

  isUppercaseValid: boolean = false;
  isLowercaseValid: boolean = false;
  isNumberValid: boolean = false;

  constructor(
    private fb: FormBuilder,
    private rest: RestService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.Form = this.fb.group({
      name: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  onSubmit(): void {
    if (this.Form.valid) {
      const user = this.Form.value;

      this.rest.signUp(user).subscribe(
        (response) => {
          console.log('Registro exitoso', response);
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Usuario registrado con éxito',
            text: 'El usuario se ha registrado correctamente.',
            showConfirmButton: false,
            timer: 1500,
          });
          this.router.navigate(['/login']);
        },
        (error) => {
          Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Error al registrar',
            text: 'Hubo un error al registrar el usuario. Intente nuevamente.',
            showConfirmButton: false,
            timer: 2000,
          });
          console.error('Error al registrar', error);
        }
      );
    }
  }

  checkUppercase(): void {
    const password = this.Form.get('password')?.value;
    this.isUppercaseValid = /[A-Z]+/.test(password);
  }

  checkLowercase(): void {
    const password = this.Form.get('password')?.value;
    this.isLowercaseValid = /[a-z]+/.test(password);
  }

  checkNumber(): void {
    const password = this.Form.get('password')?.value;
    this.isNumberValid = /[0-9]+/.test(password);
  }
}
