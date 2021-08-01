import { Component, OnInit } from '@angular/core';
import { CurrentUser } from 'src/app/models/current-user.model';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  public currentUser: CurrentUser;

  constructor(private accountService: AccountService) {
    this.currentUser = this.accountService.getCurrentUser();
  }

  ngOnInit(): void { }

}
