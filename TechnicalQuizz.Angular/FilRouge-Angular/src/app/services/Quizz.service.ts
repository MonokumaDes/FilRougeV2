import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs/Subscription';
import { Route } from '@angular/router';

@Injectable()
export class QuizzService {

     Quizz: any;

constructor(private httpClient: HttpClient) { }


    getQuizzFromApi(id) {
        this.httpClient
        .get('http://localhost:61251/api/quizz/' + id)
        .subscribe(
            (response) => {

                this.Quizz = response;

             }, (error) => {
                 console.log('Une erreur ' + error);
             }
        );
    }
}
