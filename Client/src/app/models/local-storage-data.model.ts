import * as Interfaces from "../interfaces/local-storage-data.interface";

export class LocalStorageData<T> implements Interfaces.LocalStorageData<T> {

    public constructor(
        public key: string,
        public value: T
    ) { }
}