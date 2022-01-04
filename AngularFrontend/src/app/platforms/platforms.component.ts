import { Component, OnInit } from '@angular/core';
import { PlatformService } from '../platform.service';
import { PlatformReadDto } from '../models/PlatformReadDto';

@Component({
  selector: 'app-platforms',
  templateUrl: './platforms.component.html',
  styleUrls: ['./platforms.component.less']
})

export class PlatformsComponent implements OnInit {

  constructor(private platformService : PlatformService) { }

  platforms?: PlatformReadDto[];

  ngOnInit(): void {
    this.getPlatforms();
  }

  private getPlatforms(){
    this.platformService.getPlatforms().subscribe(platforms => {
      this.platforms = platforms;
    })
  }

}
