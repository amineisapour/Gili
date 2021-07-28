import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'numberToWord'
})
export class NumberToWordPipe implements PipeTransform {
  transform(value: any): string {
    if (value && isInteger(value))
      return numToWords(value);

    return value;
  }
}

const isInteger = function (x: any) {
  return x % 1 === 0;
}

const arr = (x: any) => Array.from(x);
const num = (x: any) => Number(x) || 0;
const str = (x: any) => String(x);
const isEmpty = (xs: any) => xs.length === 0;
const take = (n: any) => (xs: any) => xs.slice(0, n);
const drop = (n: any) => (xs: any) => xs.slice(n);
const reverse = (xs: any) => xs.slice(0).reverse();
const comp = (f: any) => (g: any) => (x: any) => f(g(x));
const not = (x: any) => !x;
const chunk: any = (n: any) => (xs: any) => isEmpty(xs) ? [] : [take(n)(xs), ...chunk(n)(drop(n)(xs))];

// numToWords :: (Number a, String a) => a -> String
let numToWords: any = (n: any) => {
  let a = [
    '', 'one', 'two', 'three', 'four',
    'five', 'six', 'seven', 'eight', 'nine',
    'ten', 'eleven', 'twelve', 'thirteen', 'fourteen',
    'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'
  ];
  let b = [
    '', '', 'twenty', 'thirty', 'forty',
    'fifty', 'sixty', 'seventy', 'eighty', 'ninety'
  ];
  let g = [
    '', 'thousand', 'million', 'billion', 'trillion', 'quadrillion',
    'quintillion', 'sextillion', 'septillion', 'octillion', 'nonillion'
  ];
  // this part is really nasty still
  // it might edit this again later to show how Monoids could fix this up
  let makeGroup = ([ones, tens, huns]: [any, any, any]) => {
    return [
      num(huns) === 0 ? '' : a[huns] + ' hundred ',
      num(ones) === 0 ? b[tens] : b[tens] && b[tens] + '-' || '',
      a[tens + ones] || a[ones]
    ].join('');
  };
  // "thousands" constructor; no real good names for this, i guess
  let thousand = (group: any, i: any) => group === '' ? group : `${group} ${g[i]}`;
  // execute !
  if (typeof n === 'number') return numToWords(String(n));
  if (n === '0') return 'zero';
  return comp(chunk(3))(reverse)(arr(n))
    .map(makeGroup)
    .map(thousand)
    .filter(comp(not)(isEmpty))
    .reverse()
    .join(' ');
};