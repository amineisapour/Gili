import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ErrorService implements ErrorHandler {

  constructor(private injector: Injector) { }

  handleError(error: any): void {
    //throw new Error('Method not implemented.');
    const router = this.injector.get(Router);
    if (Error instanceof HttpErrorResponse) {
      console.log(error.status);
      console.log(error.message);
    }
    else {
      //console.log(error.message);
      //console.error("An error occurred here!");
      console.error(router.url);
      console.error(error);
    }
    //router.navigate(['/error']);
  }
}
