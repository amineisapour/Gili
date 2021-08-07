import { HttpErrorResponse } from '@angular/common/http';
import { SnackbarComponent } from 'src/app/components/common/snackbar/snackbar.component';
import { HttpRequestResult } from 'src/app/interfaces/http-request-result.interface';
import { MessageType } from 'src/app/models/enums/enums';

export class ErrorHandleHelper {
    public static handleError(error: HttpErrorResponse, snackbar: SnackbarComponent): void {
        try {
          let httpRequestResult = error.error as HttpRequestResult<any>;
          if (httpRequestResult != undefined && httpRequestResult != null) {
            if (httpRequestResult.isFailed) {
              httpRequestResult.errors.forEach(function (item, index) {
                console.error(item);
              });
              snackbar.openSnackBar(httpRequestResult.errors, MessageType.Error);
            } else if (httpRequestResult.isSuccess) {
              httpRequestResult.successes.forEach(function (item, index) {
                console.error(item);
              });
              snackbar.openSnackBar(httpRequestResult.successes, MessageType.Error);
            } else {
              console.error(error.message);
              snackbar.openSnackBar(error.message, MessageType.Error);
            }
          } else {
            console.error(error.message);
            snackbar.openSnackBar(error.message, MessageType.Error);
          }
        }
        catch (error) {
          console.error(error);
          snackbar.openSnackBar(error.message, MessageType.Error);
        }
      }
}