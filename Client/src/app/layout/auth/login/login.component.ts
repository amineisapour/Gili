import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidationService } from 'src/app/services/common/validation.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;
  data: any = '';
  hide = true;

  constructor(private formBuilder: FormBuilder) {
    this.loginForm = formBuilder.group({
      email: ['', [Validators.required, ValidationService.emailValidator]],
      password: ['', [Validators.required, ValidationService.passwordValidator]]
    });
  }

  ngOnInit(): void { }

  getErrorMessage(element: string): string {
    return this.loginForm.controls[element].hasError('required') ? 'The ' + element + ' is required!' :
      this.loginForm.controls[element].hasError('invalidEmailAddress') ? 'Not a valid ' + element + '!' :
        this.loginForm.controls[element].hasError('invalidPassword') ? 'At least 8 characters long including uppercase, lowercase, numeric, and special character.' :
          '';
  }

  login(data: any): void {
    this.data = data;
    alert(this.data.email + '\r\n' + this.data.password);
  }

}
