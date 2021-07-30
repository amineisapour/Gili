import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { HttpRequestResult } from 'src/app/interfaces/http-request-result.interface';
import { AuthenticateData } from 'src/app/models/authenticate-data.model';
import { CurrentUser } from 'src/app/models/current-user.model';
import { LocalStorageData } from 'src/app/models/local-storage-data.model';
import { AccountService } from 'src/app/services/account.service';
import { LocalStorageService } from 'src/app/services/common/local-storage.service';
import { ValidationService } from 'src/app/services/common/validation.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;
  public returnUrl: string = '/';
  //public data: any;
  public hide = true;
  myInfo$ = this.localStorageService.myData$;

  constructor(private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private localStorageService: LocalStorageService) {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required, ValidationService.emailValidator]],
      password: ['', [Validators.required, ValidationService.passwordValidator]]
    });
  }

  ngOnInit(): void {
    if (this.accountService.isAuthenticat) {
      this.router.navigate(['/']);
      return;
    }

    // let returnUrl = this.activatedRoute.snapshot.params.returnUrl;
    // if (returnUrl != null) {
    //   this.returnUrl = returnUrl;
    // }

    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.returnUrl = params['returnUrl'];
    });

    // this.activatedRoute.params.subscribe((params: Params) => {
    //   if (params.returnUrl) {
    //     this.returnUrl = params.returnUrl;
    //   }
    // });

    this.localStorageService.clearAllLocalStorage();
  }

  getErrorMessage(element: string): string {
    return this.loginForm.controls[element].hasError('required') ? 'The ' + element + ' is required!' :
      this.loginForm.controls[element].hasError('invalidEmailAddress') ? 'Not a valid ' + element + '!' :
        this.loginForm.controls[element].hasError('invalidPassword') ? 'At least 8 characters long including uppercase, lowercase, numeric, and special character.' :
          '';
  }

  login(data: any): void {
    //this.data = data;
    //alert(this.data.username + '\r\n' + this.data.password);

    this.accountService.login(data).subscribe(
      (result: HttpRequestResult<AuthenticateData>) => {
        if (result.isFailed) {
          //alert(result.errors.forEach)
          result.errors.forEach(function (item, index) {
            console.log(item);
            alert(item)
          });
        } else {
          if (result.value != null) {
            let user = new CurrentUser(
              result.value.Id,
              result.value.Username,
              result.value.Gender,
              result.value.FullName
            );
            this.localStorageService.setInfo(new LocalStorageData<CurrentUser>("CurrentUser", user));
            this.localStorageService.setInfo(new LocalStorageData<string>("Token", result.value.Token));
            this.localStorageService.setInfo(new LocalStorageData<string>("RefreshToken", result.value.RefreshToken));
          } else {
            alert('problem!');
          }
        }

        //localStorage.setItem('token', result.data.token);
        //localStorage.setItem('user-info', JSON.stringify(result.data));
        console.log(this.returnUrl);
        window.location.href = this.returnUrl;
      },
      (error: HttpErrorResponse) => {
        console.log(error.error);
        alert(error.error)
      }
    );
  }

}
