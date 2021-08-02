//? Modules
import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './modules/shared.module';

//? Components
import { AppComponent } from './app.component';
import { LayoutComponent } from './layout/layout.component';
import { AdminComponent } from './layout/admin/admin.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HeaderComponent } from './layout/admin/header/header.component';
import { FooterComponent } from './layout/admin/footer/footer.component';
import { MainComponent } from './layout/admin/main/main.component';
import { NavBarComponent } from './layout/admin/main/nav-bar/nav-bar.component';
import { LoginComponent } from './layout/auth/login/login.component';
import { RegisterComponent } from './layout/auth/register/register.component';
import { SnackbarComponent } from './components/common/snackbar/snackbar.component';
import { DialogBoxComponent } from './components/common/dialog-box/dialog-box.component';
import { LoaderComponent } from './components/common/loader/loader.component';

//? Services and Providers
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorService } from './services/common/error.service';
import { DialogBoxService } from './services/common/dialog-box.service';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { LoaderInterceptor } from './interceptors/loader.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    HeaderComponent,
    MainComponent,
    FooterComponent,
    LayoutComponent,
    NavBarComponent,
    AdminComponent,
    LoginComponent,
    RegisterComponent,
    SnackbarComponent,
    DialogBoxComponent,
    LoaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule
  ],
  providers: [
    { provide: ErrorHandler, useClass: ErrorService },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
    SnackbarComponent,
    DialogBoxService
  ],
  bootstrap: [
    AppComponent
  ],
  entryComponents: [
    DialogBoxComponent
  ],
})
export class AppModule { }
