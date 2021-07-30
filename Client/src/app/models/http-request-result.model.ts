import * as Interfaces from "../interfaces/http-request-result.interface";

export class HttpRequestResult<T> implements Interfaces.HttpRequestResult<T> {

    public constructor(
        public isFailed: boolean,
        public isSuccess: boolean,
        public errors: string[],
        public successes: string[],
        public value?:  T
    ) { }
}