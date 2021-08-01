import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SettingsService } from './common/settings.service';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpRequestResult } from '../interfaces/http-request-result.interface';
import { AuthenticateData } from '../interfaces/authenticate-data.interface';
import { LocalStorageService } from './common/local-storage.service';
import { CurrentUser } from '../interfaces/current-user.interface';


@Injectable({
    providedIn: 'root'
})
export class AccountService {

    private httpOptions;
    private baseAuthenticationUrl;

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

    public getCurrentUser(): CurrentUser {
        return this.localStorageService.loadInfo('current-user') as CurrentUser;
    }

    public isAuthenticat(): boolean {
        let token = this.getToken();
        return (token != undefined && token != '') ? true : false;
    }

    public getToken(): string {
        return this.localStorageService.loadInfo('token') as string;
    }

    public getRefreshToken(): string {
        return this.localStorageService.loadInfo('refresh-token') as string;
    }

    public logout(): void {
        this.localStorageService.clearInfo('token');
        this.localStorageService.clearInfo('current-user');
        this.localStorageService.clearInfo('refresh-token');
    }

}