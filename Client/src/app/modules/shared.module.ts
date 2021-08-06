import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { HttpClientModule } from '@angular/common/http';
import { PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface, PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { NumberToWordPipe } from '../pipes/number-to-word.pipe';
import { GetFullnameWithGenderPipe } from '../pipes/get-fullname-with-gender.pipe';
import { GetFormatDateTimePipe } from '../pipes/get-format-date-time.pipe';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    NumberToWordPipe,
    GetFullnameWithGenderPipe,
    GetFormatDateTimePipe
  ],
  imports: [
    CommonModule,
    PerfectScrollbarModule,
    HttpClientModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    NumberToWordPipe,
    GetFullnameWithGenderPipe,
    GetFormatDateTimePipe,
    MaterialModule,
    PerfectScrollbarModule,
    HttpClientModule
  ],
  providers: [
    //? Perfect Scrollbar
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})
export class SharedModule { }
