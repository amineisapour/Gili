import * as Interfaces from "../interfaces/menu-item.interface";
import { Nullable } from "./base.model";

export class MenuItem implements Interfaces.MenuItem {

    public constructor(
        public id: number,
        public title: string,
        public icon: string,
        public path: string,
        public lineNeed: boolean,
        public children?: Nullable<MenuItem[]>
    ) { }
}
