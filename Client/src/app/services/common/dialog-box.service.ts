import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { DialogBoxComponent } from 'src/app/components/common/dialog-box/dialog-box.component';
import { DialogData } from 'src/app/models/dialog/dialog-data.model';

@Injectable()
export class DialogBoxService {

    private dialogRef: any;

    constructor(public dialog: MatDialog) { }

    openDialog(
        message: string,
        title?: string,
        showOKBtn?: boolean,
        showCancelBtn?: boolean,
        additionalDialogConfigData?: MatDialogConfig
    ): MatDialogRef<DialogBoxComponent> {

        let data: DialogData = new DialogData(
            (title != null) ? title : '',
            message,
            showOKBtn ?? true,
            showCancelBtn ?? true
        );

        let dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.data = data;
        //dialogConfig.width = (width != null && width != '') ? width : '500px';
        dialogConfig.autoFocus = true;
        dialogConfig.disableClose = true;

        let config: MatDialogConfig = dialogConfig;
        if (additionalDialogConfigData != null) {
            config = Object.assign(dialogConfig, additionalDialogConfigData);
        }

        if (this.dialogRef) {
            this.dialogRef.close();
        }
        this.dialogRef = this.dialog.open(DialogBoxComponent, config);

        this.dialogRef.afterClosed().subscribe((result: MatDialogRef<DialogBoxComponent>) => { });

        return this.dialogRef;
    }
}