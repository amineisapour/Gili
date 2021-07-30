export interface HttpRequestResult<T> {
    isFailed: boolean;
    isSuccess: boolean;
    errors: string[];
    successes: string[];
    value?: T;
}