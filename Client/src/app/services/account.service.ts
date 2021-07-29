import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SettingsService } from './common/settings.service';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpRequestResult } from '../interfaces/http-request-result.interface';
import { AuthenticateData } from '../interfaces/authenticate-data.interface';

@Injectable({
    providedIn: 'root'
})
export class AccountService {

    private httpOptions;
    private baseAuthenticationUrl;

    constructor(private http: HttpClient,
        private settingsService: SettingsService) {
        this.httpOptions = this.settingsService.httpOptions;
        this.baseAuthenticationUrl = this.settingsService.baseAuthenticationUrl;
    }

    public login(model: any): Observable<HttpRequestResult<AuthenticateData>> {
        let requestUrl: string = this.baseAuthenticationUrl + 'auth/login';
        return this.http.post<HttpRequestResult<AuthenticateData>>(requestUrl, model, this.httpOptions)
            .pipe(
                map(result => { return result; }),
                catchError(this.errorHandler)
            );
    }

    errorHandler(error: HttpErrorResponse) {
        return Observable.throw(error.message || "server error.");
    }

}