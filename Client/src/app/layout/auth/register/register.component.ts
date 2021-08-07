import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { ValidationService } from 'src/app/services/common/validation.service';
import { AppDateAdapter, AppDateTime, APP_DATE_FORMATS } from 'src/app/infrastructure/helpers/format-datepicker.helper';
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { SnackbarComponent } from 'src/app/components/common/snackbar/snackbar.component';
import { DateTimeFormat, Gender, MessageType } from 'src/app/models/enums/enums';
import { HttpRequestResult } from 'src/app/models/http-request-result.model';
import { AuthenticateData } from 'src/app/models/authenticate-data.model';
import { LocalStorageService } from 'src/app/services/common/local-storage.service';
import { CurrentUser } from 'src/app/models/current-user.model';
import { LocalStorageData } from 'src/app/models/local-storage-data.model';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandleHelper } from 'src/app/infrastructure/helpers/error-handle.helper';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  providers: [
    { provide: DateAdapter, useClass: AppDateAdapter },
    { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }
  ]
})
export class RegisterComponent implements OnInit {

  public registerForm: FormGroup;
  public returnUrl: string = '/';
  public hide = true;
  public hideConfirm = true;
  //userGender: typeof Gender = Gender;
  userGender = Gender;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private formBuilder: FormBuilder,
    private localStorageService: LocalStorageService,
    public snackbar: SnackbarComponent
  ) {
    this.registerForm = this.formBuilder.group({
      username: ['', [Validators.required, ValidationService.emailValidator]],
      password: ['', [Validators.required, ValidationService.passwordValidator]],
      confirmPassword: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      firstName: ['', [Validators.required, Validators.maxLength(50)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      nationalId: ['', ValidationService.nationalIdValidator],
      birthdate: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    if (this.accountService.isAuthenticat()) {
      this.router.navigate(['/']);
      return;
    }
  }

  getErrorMessage(element: string): string {
    return this.registerForm.controls[element].hasError('required') ? 'The ' + element + ' is required!' :
      this.registerForm.controls[element].hasError('invalidEmailAddress') ? 'Not a valid ' + element + '!' :
        this.registerForm.controls[element].hasError('invalidPassword') ? 'At least 8 characters long including uppercase, lowercase, numeric, and special character.' :
          this.registerForm.controls[element].hasError('invalidNationalId') ? 'National ID must be a number and min length and max length must be 10!' :
            this.registerForm.controls[element].hasError('pattern') ? 'Passwords do not match' :
              '';
  }

  register(data: any): void {
    const model = {
      "Username": data.username,
      "Password": data.password,
      "FirstName": data.firstName,
      "LastName": data.lastName,
      "NationalId": data.nationalId,
      "Gender": (data.gender == Gender.Man) ? 1 : 0,
      "Birthdate": AppDateTime.getFormatDateTime(data.birthdate, DateTimeFormat.YyyyMmDd)
    };
    this.accountService.register(model).subscribe(
      (result: HttpRequestResult<AuthenticateData>) => {
        if (result.isFailed) {
          this.snackbar.openSnackBar(result.errors, MessageType.Error);
        } else {
          if (result.value != null) {
            let user = new CurrentUser(
              result.value.id,
              result.value.username,
              result.value.gender,
              result.value.fullName
            );
            this.localStorageService.setInfo(new LocalStorageData<CurrentUser>("current-user", user));
            this.localStorageService.setInfo(new LocalStorageData<string>("token", result.value.token));
            this.localStorageService.setInfo(new LocalStorageData<string>("refresh-token", result.value.refreshToken));

            window.location.href = this.returnUrl;
          } else {
            this.snackbar.openSnackBar('problem!', MessageType.Error);
          }
        }
      },
      (error: HttpErrorResponse) => {
        return ErrorHandleHelper.handleError(error, this.snackbar);
      }
    );
  }

  login(): void {
    this.router.navigate(['/auth/login']);
  }

}