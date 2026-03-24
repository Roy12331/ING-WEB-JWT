/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PersonaTipoSangreService } from './personaTipoSangre.service';

describe('Service: PersonaTipoSangre', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PersonaTipoSangreService]
    });
  });

  it('should ...', inject([PersonaTipoSangreService], (service: PersonaTipoSangreService) => {
    expect(service).toBeTruthy();
  }));
});
