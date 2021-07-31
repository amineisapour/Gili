import * as Interfaces from "../interfaces/authenticate-data.interface";

export class AuthenticateData implements Interfaces.AuthenticateData {

    public constructor(
        public id: string,
        public username: string,
        public gender: string,
        public fullName: string,
        public token: string,
        public refreshToken: string
    ) { }
}