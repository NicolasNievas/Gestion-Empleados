import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RestService } from '../Services/rest.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = this.fb.group({});

  isUppercaseValid: boolean = false;
  isLowercaseValid: boolean = false;
  isNumberValid: boolean = false;

  constructor(
    private fb: FormBuilder,
    private rest: RestService,
    private router: Router
    ) {}

    // En tu componente Angular
ngOnInit(): void {
  this.loginForm = this.fb.group({
    name: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(8)]],
  });
}


  onSubmit(): void {
    console.log(this.loginForm);
    if (this.loginForm.valid) {
      const credentials = this.loginForm.value;
      this.rest.login(credentials).subscribe(
        (response) => {
          console.log('Login exitoso:', response);
          this.router.navigate(['/listado']);
        },
        (error) => {
          alert('El nombre de usuario y/o la contraseña son incorrectos');
          console.error('Error en el login:', error);
        }
      );
    } else {
      alert('Formulario no válido. Revise los campos.');
      console.warn('Formulario no válido. Revise los campos.');
    }
  }

  checkUppercase(): void {
    const password = this.loginForm.get('password')?.value;
    this.isUppercaseValid = /[A-Z]+/.test(password);
  }

  checkLowercase(): void {
    const password = this.loginForm.get('password')?.value;
    this.isLowercaseValid = /[a-z]+/.test(password);
  }

  checkNumber(): void {
    const password = this.loginForm.get('password')?.value;
    this.isNumberValid = /[0-9]+/.test(password);
  }

}
