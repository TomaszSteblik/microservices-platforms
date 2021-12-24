import { Component } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Location} from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  title = 'AngularFrontend';
  position = '';

  constructor(public route:ActivatedRoute, public location:Location) {
    this.position = location.path().split('/')[1];
  }

  setPosition = (position:string) => {
    this.position = position;
  }
}
