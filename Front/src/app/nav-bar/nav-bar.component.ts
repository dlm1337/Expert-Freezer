import { Component } from '@angular/core';

import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { RegisterUserDialogComponent } from '../dialog/register-user-dialog/register-user-dialog.component';
import { LoginDialogComponent } from '../dialog/login-dialog/login-dialog.component';
import { EditProfileDialogComponent } from '../dialog/edit-profile-dialog/edit-profile-dialog.component';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})

export class NavBarComponent {
  constructor(private matDialog: MatDialog, private authService: AuthService) { }


  openRegisterDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = 'some data';
    let dialogRef = this.matDialog.open(RegisterUserDialogComponent, dialogConfig);
  }

  openLoginDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = 'some data';
    let dialogRef = this.matDialog.open(LoginDialogComponent, dialogConfig);
  }

  openEditProfileDialog() {
    if (this.authService.isLoggedIn()) {
      const dialogConfig = new MatDialogConfig();
      dialogConfig.data = 'some data';
      let dialogRef = this.matDialog.open(EditProfileDialogComponent, dialogConfig);
    }
  }

}
