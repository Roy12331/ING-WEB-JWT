import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PersonaTipoSexoDto } from '../../models/persona/PersonaTipoSexoDto.model';

@Injectable({
  providedIn: 'root'
})
export class PersonaTipoSexoService {

  http = inject(HttpClient);

  private apiUrl = 'https://localhost:7000/api/PersonaTipoSexo';


  getAll(): Observable<PersonaTipoSexoDto[]> {
    return this.http.get<PersonaTipoSexoDto[]>(this.apiUrl);
  }


}
