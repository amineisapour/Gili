import * as Interfaces from "../interfaces/menu-item.interface";

export class MenuItem implements Interfaces.MenuItem {

    public constructor(
        public id: number,
        public title: string,
        public type: 'item' | 'collapse',
        public icon: string,
        public url: string,
        public lineNeed: boolean,
        public children?: MenuItem[] | null,
    ) { }
}
