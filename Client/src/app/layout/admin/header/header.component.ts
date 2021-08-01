import { Component, EventEmitter, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { CurrentUser } from 'src/app/interfaces/current-user.interface';
import { AccountService } from 'src/app/services/account.service';
import { DialogBoxService } from 'src/app/services/common/dialog-box.service';

@Component({
  selector: 'app-admin-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HeaderComponent implements OnInit {

  public direction: string = 'menu';
  public currentUser: CurrentUser;

  constructor(
    private accountService: AccountService,
    private dialogService: DialogBoxService
  ) {
    this.currentUser = this.accountService.getCurrentUser();
  }

  @Output() sideNavToggle = new EventEmitter();

  ngOnInit(): void {
  }

  logout(): void {
    //const dialogRef = this.dialogService.openDialog(dialogData, { disableClose: true });
    const dialogRef = this.dialogService.openDialog(
      'Do you want to logout?',
      'Lougout'
    );

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        //console.log('User clicked OK');
        this.accountService.logout();
        window.location.href = '/auth/login';
      } else {
        //console.log('User clicked Cancel');
        return;
      }
    });
  }

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
