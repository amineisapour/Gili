import { Pipe, PipeTransform } from '@angular/core';
import { Gender } from '../models/enums/enums';

@Pipe({
  name: 'getFullnameWithGender'
})
export class GetFullnameWithGenderPipe implements PipeTransform {

  transform(fullname: string, gender: string | number): string {
    let result: string = '';

    switch (gender) {
      case Gender.Woman.toString():
      case 0:
        result = 'Ms. ';
        break;
      case Gender.Man.toString():
      case 1:
        result = 'Mr. ';
        break;
    }

    result += fullname;

    return result;
  }

}
