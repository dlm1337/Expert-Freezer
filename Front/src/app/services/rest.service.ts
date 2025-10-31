import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ConfigService } from './config.service';
import { AuthService } from './auth.service';
import { ExpertFreezerProfile } from './expert-freezer-profile.model';

@Injectable({
  providedIn: 'root',
})



export class RestService {
  private url = '';


  private message = new BehaviorSubject('init msg');
  getMessage = this.message.asObservable();


  constructor(private http: HttpClient, private cfg: ConfigService,
    private authService: AuthService) {
    console.log(this.cfg);
    this.url = this.cfg.apiBaseUrl;
  }

  setMessage(message: string) {
    this.message.next(message);
  }


  login(req: FormData): Observable<FormData> {
    const url = `${this.url}api/login`;

    return this.http.post(url, req).pipe(
      map((resp: any) => {
        this.authService.setLoggedInId(resp.id);
        this.authService.setToken(resp.token);
        console.log(this.authService.getToken());
      }),
      catchError((error) => {
        console.log(error);
        return of(error);
      })
    );
  }

  register(req: FormData): Observable<FormData> {
    const url = `${this.url}api/register`;

    return this.http.post(url, req).pipe(
      map((resp: any) => {
        return resp;
      }),
      catchError((error) => {
        console.log(error);
        return of(error);
      })
    );
  }

  patchProfile(req: FormData): Observable<FormData> {
    const url = `${this.url}api/profile/patch`;

    return this.http.patch(url, req).pipe(
      map((resp: any) => {
        console.log("heres the address!:" + resp.address);
        return resp;
      }),
      catchError((error) => {
        console.log(error);
        return of(error);
      })
    );
  }

  getProfileById(id: string | null): Observable<ExpertFreezerProfile> {

    const url = `${this.url}api/profile/${id}`;
    return this.http.get(url).pipe(
      map((resp: any) => {
        console.log(resp);
        return resp;
      }),
      catchError((error) => {
        console.log(error);
        return of(error);
      })
    );
  }
}