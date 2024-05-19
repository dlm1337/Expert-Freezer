import { Component, OnInit } from '@angular/core';
import { FeedCardComponent } from '../feed-card/feed-card.component';

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
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.scss']
})
export class FeedComponent implements OnInit {

  tiles: Tile[] = [
    { component: FeedCardComponent, text: 'Three', cols: 1, rows: 6, color: 'lightpink' }, 
    { component: FeedCardComponent, text: 'Three', cols: 1, rows: 6, color: 'lightpink' }, 
    { component: FeedCardComponent, text: 'Three', cols: 1, rows: 6, color: 'lightpink' }, 
    { component: FeedCardComponent, text: 'Three', cols: 1, rows: 6, color: 'lightpink' }, 
    { component: FeedCardComponent, text: 'Three', cols: 1, rows: 6, color: 'lightpink' }, 
    { component: FeedCardComponent, text: 'Three', cols: 1, rows: 6, color: 'lightpink' }, 
    { component: FeedCardComponent, text: 'Three', cols: 1, rows: 6, color: 'lightpink' }, 
    { component: FeedCardComponent, text: 'Three', cols: 1, rows: 6, color: 'lightpink' }, 
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
