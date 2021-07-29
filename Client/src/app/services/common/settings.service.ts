import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class SettingsService {

    public baseAuthenticationUrl: string;

    constructor() {
        this.baseAuthenticationUrl = environment.authenticationApiUrl;
    }

    public httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/'
        })
    };

}