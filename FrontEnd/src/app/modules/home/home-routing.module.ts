import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from 'src/app/home/home/home.component';

const routes: Routes = [
  {
    path:'Home',component:HomeComponent,
    children: [
      {path:'Personas',loadChildren: () => import('../personas/personas.module').then(m => m.PersonasModule)},
      {path:'Login',loadChildren: () => import('../login/login.module').then(m => m.LoginModule)},
      {path:'**',redirectTo:'Home'}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
