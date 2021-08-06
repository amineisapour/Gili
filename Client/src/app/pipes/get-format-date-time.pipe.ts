import { Pipe, PipeTransform } from '@angular/core';
import { AppDateTime } from '../infrastructure/helpers/format-datepicker.helper';
import { DateTimeFormat, DateTimeSplitter } from 'src/app/models/enums/enums';

@Pipe({
    name: 'getFormatDateTime'
})
export class GetFormatDateTimePipe implements PipeTransform {

    transform(dateTime: string, format: DateTimeFormat | null = null, splitter: DateTimeSplitter | null = null): string {
        return AppDateTime.getFormatDateTime(dateTime, format, splitter);
    }

}