/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { QuizzService } from './Quizz.service';

describe('Service: Quizz', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [QuizzService]
    });
  });

  it('should ...', inject([QuizzService], (service: QuizzService) => {
    expect(service).toBeTruthy();
  }));
});
