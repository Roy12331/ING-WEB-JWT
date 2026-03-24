import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PersonaTipoSangreDto } from '../../models/persona/PersonaTipoSangreDto.model';

@Injectable({
  providedIn: 'root'
})
export class PersonaTipoSangreService {

  //inyectar el servicio de httpclient
  http = inject(HttpClient);

  private apiUrl = 'https://localhost:7000/api/PersonaTipoSangre';


  getAll(): Observable<PersonaTipoSangreDto[]> {
    return this.http.get<PersonaTipoSangreDto[]>(this.apiUrl);
  }




}
