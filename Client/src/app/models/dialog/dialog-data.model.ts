import * as Interfaces from "src/app/interfaces/dialog/dialog-data.interface";

export class DialogData implements Interfaces.DialogData {
    public constructor(
        public title: string,
        public message: string,
        public showOKBtn: boolean,
        public showCancelBtn: boolean
    ) { }
}