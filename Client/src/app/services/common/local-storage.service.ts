import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { LocalStorageReference } from "src/app/infrastructure/references/local-storage.reference";
import { LocalStorageData } from "src/app/models/local-storage-data.model";


@Injectable({ providedIn: "root" })
export class LocalStorageService {
    private _localStorage: Storage;

    private _myData$ = new BehaviorSubject<LocalStorageData<any> | null>(null);
    myData$ = this._myData$.asObservable();

    constructor(private localStorageRefService: LocalStorageReference) {
        this._localStorage = localStorageRefService.localStorage;
    }

    setInfo(data: LocalStorageData<any>): void {
        const jsonData = JSON.stringify(data.value);
        this._localStorage.setItem(data.key, jsonData);
        this._myData$.next(data);
    }

    loadInfo(key: string): void {
        //const data = JSON.parse(this._localStorage.getItem(key) || '{}');
        const data = JSON.parse(this._localStorage.getItem(key)!);
        this._myData$.next(data);
    }

    clearInfo(key: string) {
        this._localStorage.removeItem(key);
        this._myData$.next(null);
    }

    clearAllLocalStorage(): void {
        this._localStorage.clear();
        this._myData$.next(null);
    }
}