import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  public comment: string;

  constructor() {
    this.comment = '1234567890';
  }

  ngOnInit(): void {
  }

}
