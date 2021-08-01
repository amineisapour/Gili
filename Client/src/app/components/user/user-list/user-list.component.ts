import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MessageType } from 'src/app/models/enums/enums';
import { HttpRequestResult } from 'src/app/models/http-request-result.model';
import { User } from 'src/app/models/users/user.model';
import { AccountService } from 'src/app/services/account.service';
import { SnackbarComponent } from '../../common/snackbar/snackbar.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  displayedColumns: string[] = ['id', 'username', 'gender', 'firstName', 'lastName', 'birthdate', 'registerDateTime', 'isActive'];
  dataSource: MatTableDataSource<User>;

  @ViewChild(MatPaginator) paginator: MatPaginator | any;
  // @ViewChild(MatPaginator, { static: false }) set paginator(value: MatPaginator) {
  //   this.dataSource.paginator = value;
  // }

  @ViewChild(MatSort) sort: MatSort | any;
  // @ViewChild(MatSort, { static: false }) set sort(value: MatSort) {
  //   this.dataSource.sort = value;
  // }

  constructor(
    private accountService: AccountService,
    public snackbar: SnackbarComponent
  ) {
    this.dataSource = new MatTableDataSource<User>();
  }

  ngOnInit() {
    this.accountService.getAllUser().subscribe(
      (result: HttpRequestResult<User[]>) => {
        if (result.isFailed) {
          this.snackbar.openSnackBar(result.errors, MessageType.Error);
        }
        else {
          if (result.value != null) {
            let listUser = result.value;
            //console.table(listUser);
            this.dataSource = new MatTableDataSource(listUser);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
          } else {
            //console.log('problem!');
            this.snackbar.openSnackBar('problem!', MessageType.Error);
          }
        }
      },
      (error: HttpErrorResponse) => {
        return this.handleError(error)
      }
    );
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  handleError(error: HttpErrorResponse): void {
    try {
      let httpRequestResult = error.error as HttpRequestResult<any>;
      if (httpRequestResult != undefined && httpRequestResult != null) {
        if (httpRequestResult.isFailed) {
          httpRequestResult.errors.forEach(function (item, index) {
            console.error(item);
          });
          this.snackbar.openSnackBar(httpRequestResult.errors, MessageType.Error);
        } else if (httpRequestResult.isSuccess) {
          httpRequestResult.successes.forEach(function (item, index) {
            console.error(item);
          });
          this.snackbar.openSnackBar(httpRequestResult.successes, MessageType.Error);
        } else {
          console.error(error.message);
          this.snackbar.openSnackBar(error.message, MessageType.Error);
        }
      } else {
        console.error(error.message);
        this.snackbar.openSnackBar(error.message, MessageType.Error);
      }
    }
    catch (error) {
      console.error(error);
      this.snackbar.openSnackBar(error.message, MessageType.Error);
    }
  }

}
