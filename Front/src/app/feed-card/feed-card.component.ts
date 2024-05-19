import { Component, OnInit } from '@angular/core';

import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';


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
