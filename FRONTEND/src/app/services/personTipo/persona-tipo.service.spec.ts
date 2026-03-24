/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PersonaTipoService } from './persona-tipo.service';

describe('Service: PersonaTipo', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PersonaTipoService]
    });
  });

  it('should ...', inject([PersonaTipoService], (service: PersonaTipoService) => {
    expect(service).toBeTruthy();
  }));
});
