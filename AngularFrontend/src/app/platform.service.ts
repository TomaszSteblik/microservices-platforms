import { Injectable } from '@angular/core';
import { PlatformCreateDto } from './models/PlatformCreateDto';
import { PlatformReadDto } from './models/PlatformReadDto';
import { Observable, of} from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class PlatformService {

  api :string;

  constructor(private http: HttpClient) {
    this.api = environment.platformService;
  }

  public getPlatforms() : Observable<PlatformReadDto[]> {
    return this.http.get<PlatformReadDto[]>(`${this.api}/Platform`);
  }

  public getPlatform(id:number) : Observable<PlatformReadDto> {
    return this.http.get<PlatformReadDto>(`${this.api}/Platform/${id}`);
  }

  public createPlatform(platform : PlatformCreateDto) : Observable<PlatformReadDto> {

    var httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    return this.http.post<PlatformReadDto>(`${this.api}/Platform`, platform, httpOptions);
  }

}
