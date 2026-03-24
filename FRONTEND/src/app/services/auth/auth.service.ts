import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { URLS } from '../../Constants/url.constants';
import { LoginRequest } from '../../models/auth/login-request';
import { Observable } from 'rxjs';
import { LoginResponse } from '../../models/auth/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  //inyectar el servicio de httpclient
  http = inject(HttpClient);

  private apiUrl = URLS.auth;

  constructor() { }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.apiUrl, request);
  }

}
