import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { PersonaDto } from '../../models/persona/PersonaDto.model';
import { Observable } from 'rxjs';
import { URLS } from '../../Constants/url.constants';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {

  http = inject(HttpClient);
  private apiUrl = URLS.persona;

  constructor() { }

  getAll(): Observable<PersonaDto[]> {
    return this.http.get<PersonaDto[]>(this.apiUrl);
  }

  create(data: PersonaDto): Observable<PersonaDto> {
    return this.http.post<PersonaDto>(this.apiUrl, data);
  }

  update(id: number, data: PersonaDto): Observable<PersonaDto> {
    // ELIMINADA LA BARRA: apiUrl ya trae la barra final
    const url = `${this.apiUrl}${id}`; 
    return this.http.put<PersonaDto>(url, data);
  }

  delete(id: number): Observable<void> {
    // ELIMINADA LA BARRA: apiUrl ya trae la barra final
    const url = `${this.apiUrl}${id}`;
    return this.http.delete<void>(url);
  }
}
