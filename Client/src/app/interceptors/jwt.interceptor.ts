import { Injectable, Injector } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpInterceptor,
  HttpSentEvent,
  HttpHeaderResponse,
  HttpProgressEvent,
  HttpResponse,
  HttpUserEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError as observableThrowError, BehaviorSubject } from 'rxjs';
import { AccountService } from '../services/account.service';
import { catchError, switchMap, finalize, filter, take } from 'rxjs/operators';
import { HttpRequestResult } from '../models/http-request-result.model';
import { AuthenticateData } from '../models/authenticate-data.model';
import { LocalStorageService } from '../services/common/local-storage.service';
import { LocalStorageData } from '../models/local-storage-data.model';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  isRefreshingToken: boolean = false;
  tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor(private injector: Injector) { }

  addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({
      setHeaders: { Authorization: 'Bearer ' + token }
    });
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler):
    Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {

    const accountService = this.injector.get(AccountService);

    return next.handle(this.addToken(request, accountService.getToken())).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse) {
          switch ((<HttpErrorResponse>error).status) {
            case 400:
              return this.handle400Error(error);
            case 401:
              return this.handle401Error(request, next);
            default:
              return observableThrowError(error);
          }
        } else {
          return observableThrowError(error);
        }
      }));
  }

  logoutUser() {
    // Route to the login page (implementation up to you)
    const accountService = this.injector.get(AccountService);
    accountService.logout();
    window.location.href = '/auth/login';
    return observableThrowError("");
  }

  handle400Error(error: any) {
    if (error && error.status === 400 && error.error && error.error.error === 'invalid_grant') {
      // If we get a 400 and the error message is 'invalid_grant', the token is no longer valid so logout.
      return this.logoutUser();
    }

    return observableThrowError(error);
  }

  handle401Error(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshingToken) {
      this.isRefreshingToken = true;
      this.tokenSubject.next('');

      const accountService = this.injector.get(AccountService);
      const localStorageService = this.injector.get(LocalStorageService);

      return accountService.refresh().pipe(
        switchMap((result: HttpRequestResult<AuthenticateData>) => {
          if (result.isFailed) {
            return this.logoutUser();
          } else {
            if (result.value == null) {
              return this.logoutUser();
            } else {
              let token = result.value.token;
              let refreshToken = result.value.refreshToken;

              localStorageService.setInfo(new LocalStorageData<string>("token", token));
              localStorageService.setInfo(new LocalStorageData<string>("refresh-token", refreshToken));

              this.tokenSubject.next(token);
              return next.handle(req.clone({ setHeaders: { Authorization: 'Bearer ' + token } }));
            }
          }
        }),
        catchError(error => {
          // If there is an exception calling 'refreshToken', bad news so logout.
          return this.logoutUser();
        }),
        finalize(() => {
          this.isRefreshingToken = false;
        }));
    } else {
      return this.tokenSubject.pipe(
        filter(token => (token != null && token != '')),
        take(1),
        switchMap(token => {
          return next.handle(req.clone({ setHeaders: { Authorization: 'Bearer ' + token } }));
        }));
    }
  }

}
