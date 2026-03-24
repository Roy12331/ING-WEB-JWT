/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PersonaTipoSexoService } from './PersonaTipoSexo.service';

describe('Service: PersonaTipoSexo', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PersonaTipoSexoService]
    });
  });

  it('should ...', inject([PersonaTipoSexoService], (service: PersonaTipoSexoService) => {
    expect(service).toBeTruthy();
  }));
});
