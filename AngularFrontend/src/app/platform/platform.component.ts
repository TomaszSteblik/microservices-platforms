import { Component, OnInit, Input } from '@angular/core';
import { PlatformReadDto } from '../models/PlatformReadDto';


@Component({
  selector: 'app-platform',
  templateUrl: './platform.component.html',
  styleUrls: ['./platform.component.less']
})
export class PlatformComponent implements OnInit {

  constructor() { }

  @Input() platform? : PlatformReadDto;

  ngOnInit(): void {
  }

}
