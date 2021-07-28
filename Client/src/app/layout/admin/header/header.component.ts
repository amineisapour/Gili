import { Component, EventEmitter, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

@Component({
  selector: 'app-admin-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HeaderComponent implements OnInit {

  public direction: string = 'menu';

  constructor() { }

  ngOnInit(): void {
  }

  @Output() sideNavToggle = new EventEmitter();

  toggle() {
    this.sideNavToggle.emit();
  }

  mouseEnter() {
    this.direction = 'menu_open';
  }

  mouseLeave() {
    this.direction = 'menu';
  }

  private m_scrollbarConfiguration: PerfectScrollbarConfigInterface = {
    swipeEasing: true,
  };
  // Get perfect scrollbar configuration   
  public get config(): PerfectScrollbarConfigInterface {
    return this.m_scrollbarConfiguration;
  }
}
