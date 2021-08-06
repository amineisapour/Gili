import { Pipe, PipeTransform } from '@angular/core';
import { DateTimeFormat } from '../models/enums/enums';

@Pipe({
    name: 'getFormatDateTime'
})
export class GetFormatDateTimePipe implements PipeTransform {

    transform(dateTime: string, format: DateTimeFormat | null = null): string {
        let result: string = '';
        if (dateTime != '') {
            let date = new Date(dateTime);
            let day = ("0" + date.getDate()).slice(-2);
            let month = ("0" + (date.getMonth() + 1)).slice(-2);
            let year = date.getFullYear();
            let hours = ("0" + date.getHours()).slice(-2);
            let minutes = ("0" + date.getMinutes()).slice(-2);
            let seconds = ("0" + date.getSeconds()).slice(-2);

            if (format == null) {
                format = DateTimeFormat.YyyyMmDdHhMmSs;
            }
            switch (format) {
                case DateTimeFormat.FullText.toString():
                    result = date.toString();
                    break;
                case DateTimeFormat.YyyyMmDd.toString():
                    result = year + "-" + month + "-" + day;
                    break;
                case DateTimeFormat.YyyyMmDdHhMm.toString():
                    result = year + "-" + month + "-" + day + " " + hours + ":" + minutes;
                    break;
                case DateTimeFormat.YyyyMmDdHhMmSs.toString():
                default:
                    result = year + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
                    break;
            }
        }
        return result;
    }

}