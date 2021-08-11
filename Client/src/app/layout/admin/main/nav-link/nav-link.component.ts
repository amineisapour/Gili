import { Component, Input, OnInit } from '@angular/core';
import {
  NavigationEnd,
  Router
} from '@angular/router';
import { NavigationHelper } from 'src/app/infrastructure/helpers/navigation.helper';
import { Location } from '@angular/common';
import * as Interfaces from 'src/app/interfaces/menu-item.interface';

@Component({
  selector: 'app-admin-main-nav-link',
  templateUrl: './nav-link.component.html',
  styleUrls: ['./nav-link.component.scss']
})
export class NavLinkComponent implements OnInit {

  @Input() type: string;

  private find: boolean = false;
  private myMap = new Map<number, Interfaces.MenuItem>();
  public navList = new Map<number, Interfaces.MenuItem>();
  public mainTitle: string = '';

  constructor(private route: Router, public nav: NavigationHelper, public location: Location) {
    let routerUrl: string;
    this.route.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        routerUrl = event.urlAfterRedirects;
        //let activeLink = event.url.substring(1);
        let activeLink = routerUrl.substring(1);
        //console.log(activeLink);
        //
        this.find = false;
        this.myMap = new Map<number, Interfaces.MenuItem>();
        this.navList = new Map<number, Interfaces.MenuItem>();
        this.mainTitle = '';
        //
        //
        this.nav.get().forEach((item: Interfaces.MenuItem) => {
          if (!this.find) {
            this.navigateToNode(item, activeLink);
          }
        }
        );
        if (this.find) {
          //let mapAsc = new Map([...this.myMap.entries()].sort());
          // this.navList = this
          //   .sortedMap(this.myMap)
          //   .forEach((value: Interfaces.MenuItem, key: number) => {
          //     console.log(key);
          //     console.log(value);
          //   });
          this.navList = this.sortedMap(this.myMap);
          this.mainTitle = Array.from(this.navList)[this.navList.size - 1][1].title;
        }
      }
    });
  }

  sortedMap(object: Map<number, Interfaces.MenuItem>): any {
    return new Map([...object].sort(([k, v], [k2, v2]) => {
      if (v.id > v2.id) {
        return 1;
      }
      if (v.id < v2.id) {
        return -1;
      }
      return 0;
    }));
  }

  ngOnInit(): void { }

  navigateToNode(node: Interfaces.MenuItem, activeLink: string) {
    if (node.url === activeLink) {
      this.find = true;
      this.myMap.set(node.id, node);
      return;
    }
    node.children?.forEach((value: Interfaces.MenuItem) => {
      if (!this.find) {
        if (value.url === activeLink) {
          this.find = true;
          this.myMap.set(node.id, node);
          this.myMap.set(value.id, value);
          return;
        }
        if (value.children != null) {
          this.navigateToNode(value, activeLink);
          if (this.find) {
            this.myMap.set(node.id, node);
            this.myMap.set(value.id, value);
          }
          return;
        }
      }
    });
  }

}
