import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './layout/auth/login/login.component';
import { RegisterComponent } from './layout/auth/register/register.component';
import { NotFoundComponent } from './layout/maintenance/not-found/not-found.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'admin/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'admin',
    children: [
      {
        path: 'dashboard',
        //canActivate: [AuthGuard],
        component: DashboardComponent,
        pathMatch: 'full'
      },
      {
        path: 'user',
        loadChildren: () => import('./components/user/user.module').then(module => module.UserModule)
      },
    ]
  },
  {
    path: 'auth',
    children: [
      {
        path: 'login',
        component: LoginComponent,
        pathMatch: 'full'
      },
      {
        path: 'register',
        component: RegisterComponent,
        pathMatch: 'full'
      },
    ]
  },
  {
    path: '',
    children: [
      {
        path: 'maintenance',
        loadChildren: () => import('./layout/maintenance/maintenance.module').then(module => module.MaintenanceModule)
      },
    ]
  },
  {
    path: '**',
    component: NotFoundComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
