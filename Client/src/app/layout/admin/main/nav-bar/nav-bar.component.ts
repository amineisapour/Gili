import { Component, OnInit } from '@angular/core';
import * as Models from 'src/app/models/menu-item.model';
import * as Interfaces from 'src/app/interfaces/menu-item.interface';


@Component({
  selector: 'app-admin-main-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  fillerNav = Array.from({ length: 50 }, (_, i) => `Nav Item ${i + 1}`);

  public menuItems: Interfaces.MenuItem[];

  public constructor() {
    this.menuItems = [
      new Models.MenuItem(1, 'Dashboard', '', 'admin/dashboard', true),
      new Models.MenuItem(2, 'Tasks', '', '', true, [
        new Models.MenuItem(3, 'Tasks Board', '', 'admin/tasks/board', false),
        new Models.MenuItem(4, 'Tasks Departments', '', '', false, [
          new Models.MenuItem(5, 'Tasks DV', '', 'admin/tasks/departments/dv', false),
          new Models.MenuItem(6, 'Tasks Admin', '', 'admin/tasks/departments/admin', false),
          new Models.MenuItem(7, 'Tasks FC', '', 'admin/tasks/departments/fc', false),
        ]),
        new Models.MenuItem(8, 'Tasks Report', '', '', false, [
          new Models.MenuItem(9, 'Report 1', '', 'admin/tasks/report/1', false),
          new Models.MenuItem(10, 'Report 2', '', 'admin/tasks/report/2', false),
          new Models.MenuItem(11, 'Report 3', '', 'admin/tasks/report/3', false),
        ]),
      ]),
      new Models.MenuItem(12, 'User', '', '', false, [
        new Models.MenuItem(13, 'Profile', '', 'admin/user/profile', false),
        new Models.MenuItem(14, 'User List', '', 'admin/user/list', false),
        new Models.MenuItem(15, 'User Access', '', 'admin/user/access', false),
      ]),
    ];
  }

  ngOnInit(): void { }

  public trackByFn = (index: number, item: any) => item;

}
