import { Component, OnInit } from '@angular/core';
import { CurrentUser } from 'src/app/models/current-user.model';
import { LocalStorageService } from 'src/app/services/common/local-storage.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  public currentUser: CurrentUser;

  constructor(private localStorageService: LocalStorageService) {
    this.currentUser = this.localStorageService.loadInfo('current-user') as CurrentUser;
  }

  ngOnInit(): void { }

}
