import { Component, OnInit } from '@angular/core';
import { AuthService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'GestionEmpleados';
  isAuthenticated: boolean = true;

  constructor(public authService: AuthService) {}

  ngOnInit() {
    this.authService.isLoggedIn$.subscribe((loggedIn) => {
      this.isAuthenticated = loggedIn;
    });
  }

  get isLoggedIn$() {
    return this.authService.isLoggedIn$;
  }

  get currentUserName$() {
    return this.authService.currentUserName$;
  }

  logout() {
    this.authService.logout();
  }
}
