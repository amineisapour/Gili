import { Component, Input, OnInit } from '@angular/core';
import { delay } from 'rxjs/operators';
import { LoaderColor, LoaderType } from 'src/app/models/enums/enums';
import { LoaderService } from 'src/app/services/common/loader.service';

@Component({
  selector: 'app-common-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent implements OnInit {

  public loading: boolean = false;
  public className: string;
  public colorName: string;

  @Input() type: LoaderType;
  @Input() color: LoaderColor;

  constructor(private loaderService: LoaderService) { }

  ngOnInit(): void {
    this.className = 'loader-container';
    this.colorName = LoaderColor.Primary.toString();

    if (this.type != undefined) {
      switch (this.type) {
        case LoaderType.FullPage:
          this.className = 'loader-container';
          break;
        case LoaderType.PartialPage:
          this.className = 'loader-container-partial';
          break;
      }
    }

    if (this.color != undefined) {
      this.colorName = this.color.toString();
    }

    this.listenToLoading();
  }

  listenToLoading(): void {
    this.loaderService.loadingSub
      .pipe(delay(0)) // This prevents a ExpressionChangedAfterItHasBeenCheckedError for subsequent requests
      .subscribe((loading) => {
        this.loading = loading;
      });
  }

}
