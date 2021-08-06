import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { ValidationService } from 'src/app/services/common/validation.service';
import { SnackbarComponent } from 'src/app/components/common/snackbar/snackbar.component';
import { MessageType } from 'src/app/models/enums/enums';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public registerForm: FormGroup;
  public hide = true;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.registerForm = this.formBuilder.group({
      username: ['', [Validators.required, ValidationService.emailValidator]],
      password: ['', [Validators.required, ValidationService.passwordValidator]],
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
          this.registerForm.controls[element].hasError('invalidNationalId') ? 'Invalid National ID (minlength and maxlength is 10)!' :
            '';
  }

  register(data: any): void { }

}
