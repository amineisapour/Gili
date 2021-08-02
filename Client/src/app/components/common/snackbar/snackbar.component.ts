import { Component, OnInit } from '@angular/core';
import {
  MatSnackBar,
  MatSnackBarConfig,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition
} from '@angular/material/snack-bar';
import { MessageType } from 'src/app/models/enums/enums';

@Component({
  selector: 'app-common-snackbar',
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.scss']
})
export class SnackbarComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(private snackBar: MatSnackBar) { }

  ngOnInit(): void { }


  openSnackBar(message: any, type: MessageType, duration: number = 0) {

    let panelClass = '';
    switch (type) {
      case MessageType.Error:
        panelClass = 'alert-error';
        break;
      case MessageType.Success:
        panelClass = 'alert-success';
        break;
      case MessageType.Warning:
        panelClass = 'alert-warning';
        break;
    }

    let action = 'X';

    let config = new MatSnackBarConfig();
    config.horizontalPosition = this.horizontalPosition;
    config.verticalPosition = this.verticalPosition;
    config.duration = duration * 1000;
    config.panelClass = [panelClass];

    if (message instanceof Array) {
      let msg = '';
      message.forEach((item, index) => {
        if (msg != '') {
          msg += '\r\n';
        }
        msg += item;
      });
      this.snackBar.open(msg, action, config);
    } else {
      this.snackBar.open(message, action, config);
    }
  }

}
