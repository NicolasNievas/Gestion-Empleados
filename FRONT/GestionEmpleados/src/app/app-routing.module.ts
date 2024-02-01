import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListadoComponent } from './listado/listado.component';
import { CreateComponent } from './create/create.component';
import { SingUpComponent } from './sing-up/sing-up.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'listado', component: ListadoComponent },
  { path: 'create', component: CreateComponent },
  { path: 'sing-up', component: SingUpComponent },
  { path: 'login', component: LoginComponent },
  { path: 'registro', component: SingUpComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
