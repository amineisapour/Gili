import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  public isAuthenticat: boolean = false;

  constructor(private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.isAuthenticat = this.accountService.isAuthenticat;
  }

}
