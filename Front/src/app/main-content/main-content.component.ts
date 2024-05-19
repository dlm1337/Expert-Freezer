import { Component, OnInit, Inject } from '@angular/core';
import { RestService } from '../services/rest.service';
//import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { NameAndAddress } from '../types/nameAndAddress';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
//import { ExampleDialogComponent } from '../dialog/example-dialog/example-dialog.component';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { FooterComponent } from '../footer/footer.component';
import { SideBarComponent } from '../side-bar/side-bar.component';
import { FeedComponent } from '../feed/feed.component';

export interface Tile {
  component?: any;
  color: string;
  cols: number;
  rows: number;
  text: string;
  position?: string;
  zindex?: number;
}

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.scss'],
})
export class MainContentComponent {
  public latestNameAndAddress: NameAndAddress;

  constructor(private matDialog: MatDialog, private restSvc: RestService) { }
  //mat-grid-list is populated from the data below. This is the main layout of the app.
  tiles: Tile[] = [
    { component: NavBarComponent, text: 'One', cols: 4, rows: 1, color: 'lightblue', position: 'fixed', zindex: 1 },
    { component: SideBarComponent, text: 'Two', cols: 1, rows: 10, color: 'lightgreen', position: 'fixed' },
    { component: FeedComponent, text: 'Three', cols: 3, rows: 24, color: 'lightpink' },
    { component: FooterComponent, text: 'Four', cols: 4, rows: 2, color: '#DDBDF1' },
  ];

  // ngOnInit(): void {
  //   this.restSvc.getLatestNameAndAddress().subscribe(
  //     (nameAndAddress: NameAndAddress) => {
  //       this.latestNameAndAddress = nameAndAddress;
  //       console.log(this.latestNameAndAddress); // log the retrieved name and address
  //     },
  //     (error) => {
  //       console.log(error);
  //     }
  //   );
  // }

  // openDialog() {
  //   const dialogConfig = new MatDialogConfig();
  //   dialogConfig.data = 'some data';
  //   let dialogRef = this.matDialog.open(ExampleDialogComponent, dialogConfig);
  // }
}
