import { Pipe, PipeTransform } from '@angular/core';
import { Gender } from '../models/enums/enums';

@Pipe({
  name: 'getFullname'
})
export class GetFullnamePipe implements PipeTransform {

  transform(fullname: string, gender: string): string {
    let result: string = '';

    switch (gender) {
      case Gender.Woman.toString():
        result = 'Ms. ';
        break;
      case Gender.Man.toString():
        result = 'Mr. ';
        break;
    }

    result += fullname;

    return result;
  }

}
