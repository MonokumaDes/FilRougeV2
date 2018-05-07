import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs/Subscription';
import { Router } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { ok } from 'assert';
import { QuizzComponent } from '../Quizz/Quizz.component';

@Injectable()
export class QuizzService {

    Quizz: any[];

    QuizzSubject = new Subject<any[]>();

    emitQuizzSubject() {
        this.QuizzSubject.next(this.Quizz.slice());
    }

    constructor(private httpClient: HttpClient, private router: Router) { }

    /*getQuizzFromApiHttpClient() {
        this.httpClient
        .get<any[]>('http://localhost:61251/api/quizz/')
        .subscribe(
            (response) => {
                this.Quizz = response;
                this.emitQuizzSubject();
            },
            (error) => {
                console.log('Erreur rencontrée : ' + error);
            }
        );
    }*/

    getQuizzFromApiPromise() {
        const promise = new Promise(() => {
            this.httpClient
            .get('http://localhost:61251/api/quizz/')
            .toPromise()
            .then(
                res => {
                    console.log(res);
                    // const data = res.constructor();
                    this.Quizz = <QuizzViewModel[]> res;
                    this.emitQuizzSubject();
                    console.log('ok');
                    console.log(this.Quizz);
                }
            );
        });
        return promise;
    }
}

export class QuizzViewModel {
    QuizzId: number;
    NomUser: string;
    PrenomUser: string;

    constructor(public quizzId: number, public nomUser: string, public prenomUser: string) {
        this.QuizzId = quizzId;
        this.NomUser = nomUser;
        this.PrenomUser = prenomUser;
    }
}
