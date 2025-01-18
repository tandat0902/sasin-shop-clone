import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './layouts/admin-layout/auth/auth.component';

const routes: Routes = [
  {
    path: 'account',
    component: AuthComponent,
    loadChildren: () =>
      import('@modules/admin/admin-routing.module').then(m => m.AdminRoutingModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
