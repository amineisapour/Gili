import { Injectable } from "@angular/core";
import { MatDateFormats, NativeDateAdapter } from "@angular/material/core";
import { DateTimeFormat, DateTimeSplitter } from 'src/app/models/enums/enums';

@Injectable()
export class AppDateAdapter extends NativeDateAdapter {
    format(date: Date, displayFormat: Object): string {
        if (displayFormat === 'input') {
            return AppDateTime.getFormatDateTime(date, DateTimeFormat.YyyyMmDd, DateTimeSplitter.Dash);
            // let day: string = date.getDate().toString();
            // day = +day < 10 ? '0' + day : day;
            // let month: string = (date.getMonth() + 1).toString();
            // month = +month < 10 ? '0' + month : month;
            // let year = date.getFullYear();
            // return `${year}-${month}-${day}`;
        }
        return date.toDateString();
    }
}

export const APP_DATE_FORMATS: MatDateFormats = {
    parse: {
        dateInput: { month: 'short', year: 'numeric', day: 'numeric' },
    },
    display: {
        dateInput: 'input',
        monthYearLabel: { year: 'numeric', month: 'numeric' },
        dateA11yLabel: {
            year: 'numeric', month: 'long', day: 'numeric'
        },
        monthYearA11yLabel: { year: 'numeric', month: 'long' },
    }
};

export class AppDateTime {
    public static getFormatDateTime(dateTime: string | Date, format: DateTimeFormat | null = null, splitter: DateTimeSplitter | null = null): string {
        let result: string = '';
        let date;
        if (typeof dateTime === "string") {
            date = new Date(dateTime);
        } else {
            date = dateTime;
        }
        if (dateTime != '') {
            let day = ("0" + date.getDate()).slice(-2);
            let month = ("0" + (date.getMonth() + 1)).slice(-2);
            let year = date.getFullYear();
            let hours = ("0" + date.getHours()).slice(-2);
            let minutes = ("0" + date.getMinutes()).slice(-2);
            let seconds = ("0" + date.getSeconds()).slice(-2);

            let spl = '-';
            if (splitter == null) {
                spl = DateTimeSplitter.Dash.toString();
            } else {
                spl = splitter.toString();
            }

            if (format == null) {
                format = DateTimeFormat.YyyyMmDdHhMmSs;
            }
            switch (format) {
                case DateTimeFormat.FullText.toString():
                    result = date.toString();
                    break;
                case DateTimeFormat.YyyyMmDd.toString():
                    result = year + spl + month + spl + day;
                    break;
                case DateTimeFormat.YyyyMmDdHhMm.toString():
                    result = year + spl + month + spl + day + " " + hours + ":" + minutes;
                    break;
                case DateTimeFormat.YyyyMmDdHhMmSs.toString():
                default:
                    result = year + spl + month + spl + day + " " + hours + ":" + minutes + ":" + seconds;
                    break;
            }
        }
        return result;
    }
}