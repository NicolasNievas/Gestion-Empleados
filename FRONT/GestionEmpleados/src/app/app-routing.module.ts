import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListadoComponent } from './listado/listado.component';
import { CreateComponent } from './create/create.component';

const routes: Routes = [
  {path: '', redirectTo: '/listado', pathMatch: 'full'},
  {path: 'listado', component: ListadoComponent },
  {path: 'create', component: CreateComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
