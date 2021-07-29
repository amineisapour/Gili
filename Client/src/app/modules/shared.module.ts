import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NumberToWordPipe } from '../pipes/number-to-word.pipe';
import { MaterialModule } from './material.module';
import { PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface, PerfectScrollbarModule } from 'ngx-perfect-scrollbar';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    NumberToWordPipe
  ],
  imports: [
    CommonModule,
    PerfectScrollbarModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    NumberToWordPipe,
    MaterialModule,
    PerfectScrollbarModule
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
