import * as Interfaces from "../interfaces/authenticate-data.interface";

export class AuthenticateData implements Interfaces.AuthenticateData {

    public constructor(
        public Id: string,
        public Username: string,
        public Gender: string,
        public FullName: string,
        public Token: string,
        public RefreshToken: string
    ) { }
}