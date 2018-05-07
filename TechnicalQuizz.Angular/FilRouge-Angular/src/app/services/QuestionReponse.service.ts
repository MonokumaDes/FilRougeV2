import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs/Subscription';
import { Router } from '@angular/router';
import { QuizzService } from '../services/Quizz.service';

@Injectable()
export class QuestionReponseService {

    constructor(private httpClient: HttpClient, private router: Router, private quizzService: QuizzService) {
        // Get Question + réponse -- Quizz
        // Post UserReponse --
    }

      Questions: any;
      Reponses: any;
/*
      getQuestions(index: number) {
        this.httpClient
        .get((this.quizzService.getQuizzFromApi(index)))
        .subscribe(
          (response) => {
            this.Questions = response;
          },
          (error) => {
            console.log('Une erreur est survenue' + error);
          }
        );
      }

      getReponsesByQuestions() {
        this.httpClient.get(this.Questions.Reponses)
        .subscribe(
          (response) => {
            this.Reponses = response;
          },
          (error) => {
            console.log('Erreur de récupération des réponses : ' + error);
          }
        );
      }
*/
}
