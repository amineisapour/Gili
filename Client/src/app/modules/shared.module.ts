import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NumberToWordPipe } from '../pipes/number-to-word.pipe';
import { GetFullnamePipe } from '../pipes/get-fullname.pipe';
import { MaterialModule } from './material.module';
import { HttpClientModule } from '@angular/common/http';
import { PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface, PerfectScrollbarModule } from 'ngx-perfect-scrollbar';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    NumberToWordPipe,
    GetFullnamePipe
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
    GetFullnamePipe,
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
