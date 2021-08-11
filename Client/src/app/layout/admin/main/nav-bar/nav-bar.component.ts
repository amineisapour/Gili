import { Component, OnInit } from '@angular/core';
//import * as Models from 'src/app/models/menu-item.model';
import * as Interfaces from 'src/app/interfaces/menu-item.interface';
import { CurrentUser } from 'src/app/interfaces/current-user.interface';
import { AccountService } from 'src/app/services/account.service';
import { NavigationHelper } from 'src/app/infrastructure/helpers/navigation.helper';


@Component({
  selector: 'app-admin-main-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  public currentUser: CurrentUser;
  fillerNav = Array.from({ length: 50 }, (_, i) => `Nav Item ${i + 1}`);

  public menuItems: Interfaces.MenuItem[];

  public constructor(
    private accountService: AccountService,
    public nav: NavigationHelper
  ) {
    this.currentUser = this.accountService.getCurrentUser();

    this.menuItems = this.nav.get();
    // this.menuItems = [
    //   new Models.MenuItem(1, 'Dashboard', 'item', '', 'admin/dashboard', true),
    //   new Models.MenuItem(2, 'Tasks', 'collapse', '', '', true, [
    //     new Models.MenuItem(3, 'Tasks Board', 'item', '', 'admin/tasks/board', false),
    //     new Models.MenuItem(4, 'Tasks Departments', 'collapse', '', '', false, [
    //       new Models.MenuItem(5, 'Tasks DV', 'item', '', 'admin/tasks/departments/dv', false),
    //       new Models.MenuItem(6, 'Tasks Admin', 'item', '', 'admin/tasks/departments/admin', false),
    //       new Models.MenuItem(7, 'Tasks FC', 'item', '', 'admin/tasks/departments/fc', false),
    //     ]),
    //     new Models.MenuItem(8, 'Tasks Report', 'collapse', '', '', false, [
    //       new Models.MenuItem(9, 'Report 1', 'item', '', 'admin/tasks/report/1', false),
    //       new Models.MenuItem(10, 'Report 2', 'item', '', 'admin/tasks/report/2', false),
    //       new Models.MenuItem(11, 'Report 3', 'item', '', 'admin/tasks/report/3', false),
    //     ]),
    //   ]),
    //   new Models.MenuItem(12, 'User', 'collapse', '', '', false, [
    //     new Models.MenuItem(13, 'Profile', 'item', '', 'admin/user/profile', false),
    //     new Models.MenuItem(14, 'User List', 'item', '', 'admin/user/list', false),
    //     new Models.MenuItem(15, 'User Access', 'item', '', 'admin/user/access', false),
    //   ]),
    // ];
  }

  ngOnInit(): void { }

  public trackByFn = (index: number, item: any) => item;

}
