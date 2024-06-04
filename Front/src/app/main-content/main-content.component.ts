import { Component } from '@angular/core';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { FooterComponent } from '../footer/footer.component';
import { SideBarComponent } from '../side-bar/side-bar.component';
import { FeedComponent } from '../feed/feed.component';
import { RestService } from 'src/app/services/rest.service';
import { ProfileComponent } from '../profile/profile.component';


export interface Tile {
  component?: any;
  color: string;
  cols: number;
  rows: number;
  text: string;
  position?: string;
  zindex?: number;
  boxShadow?: string;
}

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.scss'],
})
export class MainContentComponent {

  constructor(private restSvc: RestService) { }
  //mat-grid-list is populated from the data below. This is the main layout of the app.
  tiles: Tile[] = [
    {
      component: NavBarComponent, text: 'One', cols: 4, rows: 1, color: 'lightblue', position: 'fixed',
      zindex: 1000
    },
    { component: SideBarComponent, text: 'Two', cols: 1, rows: 10, color: 'lightgreen', position: 'fixed' },
    { component: FeedComponent, text: 'Three', cols: 3, rows: 24, color: 'lightpink' },
    { component: FooterComponent, text: 'Four', cols: 4, rows: 2, color: '#DDBDF1' },
  ];

  public subscription = this.restSvc.getMessage.subscribe(data => {
    console.log(data);
    if (data === "Profile") {
      this.tiles[2].component = ProfileComponent;
    }
  });

  ngOnDestroy() {
    this.subscription.unsubscribe()
  }

}
