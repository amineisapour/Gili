import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SettingsService } from './common/settings.service';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpRequestResult } from '../interfaces/http-request-result.interface';
import { AuthenticateData } from '../interfaces/authenticate-data.interface';
import { LocalStorageService } from './common/local-storage.service';

@Injectable({
    providedIn: 'root'
})
export class AccountService {

    private httpOptions;
    private baseAuthenticationUrl;

    isAuthenticat: boolean = this.localStorageService.loadInfo('token') ? true : false;

    constructor(
        private http: HttpClient,
        private settingsService: SettingsService,
        private localStorageService: LocalStorageService
    ) {
        this.httpOptions = this.settingsService.httpOptions;
        this.baseAuthenticationUrl = this.settingsService.baseAuthenticationUrl;
    }

    public login(model: any): Observable<HttpRequestResult<AuthenticateData>> {
        let requestUrl: string = this.baseAuthenticationUrl + 'auth/login';
        let requestData = { "username": model.username, "password": model.password };
        //console.log(requestUrl);
        //console.log(requestData);

        return this.http.post<HttpRequestResult<AuthenticateData>>(requestUrl, requestData, this.httpOptions)
            .pipe(
                map(result => {
                    return result;
                }),
                catchError(e => {
                    return throwError(e)
                })
            );
    }

}