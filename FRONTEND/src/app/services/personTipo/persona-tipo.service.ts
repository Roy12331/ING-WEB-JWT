import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PersonaTipoDto } from '../../models/persona/PersonaTipoDto.model';

@Injectable({
  providedIn: 'root'
})
export class PersonaTipoService {

  http = inject(HttpClient);

  private apiUrl = 'https://localhost:7000/api/PersonaTipo';


  getAll(): Observable<PersonaTipoDto[]> {
    return this.http.get<PersonaTipoDto[]>(this.apiUrl);
  }

}
