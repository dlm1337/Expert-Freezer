import { Component, OnInit } from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'app-feed-card',
  templateUrl: './feed-card.component.html',
  styleUrls: ['./feed-card.component.scss'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule]
})

export class FeedCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
