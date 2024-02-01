// auth.service.ts

import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isLoggedInSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public isLoggedIn$: Observable<boolean> = this.isLoggedInSubject.asObservable();

  private currentUserNameSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  public currentUserName$: Observable<string | null> = this.currentUserNameSubject.asObservable();

  login(username: string, password: string): void {
    // Aquí deberías realizar la lógica de autenticación y si es exitosa, establecer isLoggedIn y currentUserName
    this.isLoggedInSubject.next(true);
    this.currentUserNameSubject.next(username);
  }

  logout(): void {
    // Aquí deberías realizar la lógica de cierre de sesión y resetear isLoggedIn y currentUserName
    this.isLoggedInSubject.next(false);
    this.currentUserNameSubject.next(null);
  }
}
