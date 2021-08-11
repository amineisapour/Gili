import { Injectable } from '@angular/core';
import * as Models from 'src/app/models/menu-item.model';
import * as Interfaces from 'src/app/interfaces/menu-item.interface';

//NavigationItem => Interfaces
//Navigation => Models

const NavigationItems = [
    new Models.MenuItem(1, 'Dashboard', 'item', '', 'admin/dashboard', true, null),
    new Models.MenuItem(2, 'Tasks', 'collapse', '', '', true, [
        new Models.MenuItem(3, 'Tasks Board', 'item', '', 'admin/tasks/board', false, null),
        new Models.MenuItem(4, 'Tasks Departments', 'collapse', '', '', false, [
            new Models.MenuItem(5, 'Tasks DV', 'item', '', 'admin/tasks/departments/dv', false, null),
            new Models.MenuItem(6, 'Tasks Admin', 'item', '', 'admin/tasks/departments/admin', false, null),
            new Models.MenuItem(7, 'Tasks FC', 'item', '', 'admin/tasks/departments/fc', false, null),
        ]),
        new Models.MenuItem(8, 'Tasks Report', 'collapse', '', '', false, [
            new Models.MenuItem(9, 'Report 1', 'item', '', 'admin/tasks/report/1', false, null),
            new Models.MenuItem(10, 'Report 2', 'item', '', 'admin/tasks/report/2', false, null),
            new Models.MenuItem(11, 'Report 3', 'item', '', 'admin/tasks/report/3', false, null),
        ]),
    ]),
    new Models.MenuItem(12, 'User', 'collapse', '', '', false, [
        new Models.MenuItem(13, 'Profile', 'item', '', 'admin/user/profile', false, null),
        new Models.MenuItem(14, 'User List', 'item', '', 'admin/user/list', false, null),
        new Models.MenuItem(15, 'User Access', 'item', '', 'admin/user/access', false, null),
    ]),
];

@Injectable()
export class NavigationHelper {
    public get(): Interfaces.MenuItem[] {
        return NavigationItems;
    }
}
