import * as Interfaces from "../interfaces/current-user.interface";

export class CurrentUser implements Interfaces.CurrentUser {

    public constructor(
        public id: string,
        public username: string,
        public gender: string,
        public fullName: string
    ) { }
}
