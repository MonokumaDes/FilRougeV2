/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { QuestionReponseService } from './QuestionReponse.service';

describe('Service: QuestionReponse', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [QuestionReponseService]
    });
  });

  it('should ...', inject([QuestionReponseService], (service: QuestionReponseService) => {
    expect(service).toBeTruthy();
  }));
});
