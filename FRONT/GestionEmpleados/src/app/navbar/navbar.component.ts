import { Component } from '@angular/core';
import { AuthService } from '../Services/auth.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  isLoggedIn$: Observable<boolean> = this.authService.isLoggedIn$;
  currentUserName$: Observable<string | null> = this.authService.currentUserName$;

  constructor(private authService: AuthService, private router: Router) {}

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
