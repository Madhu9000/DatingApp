import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'users');
  }
  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }
}
