import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommandService {

  api :string;

  constructor(private http: HttpClient) {
    this.api = environment.platformService;
  }
}
