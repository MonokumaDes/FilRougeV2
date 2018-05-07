import { Component, OnInit, Input } from '@angular/core';
import { QuizzService, QuizzViewModel } from '../services/Quizz.service';
import { Subscription } from 'rxjs/Subscription';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-quizz',
  templateUrl: './Quizz.component.html',
  styleUrls: ['./Quizz.component.css']
})

export class QuizzComponent implements OnInit {

  constructor(private quizzService: QuizzService) { }

  quizz: QuizzViewModel[];
  quizzSubscription: Subscription;

  @Input() QuizzId: number;
  @Input() PrenomUser: string;
  @Input() NomUser: string;
  index = 0;
  // QuizzTmp: any;

  ngOnInit() {
  }

  onRecup() {
    this.quizzService.getQuizzFromApiPromise();
    this.quizzSubscription = this.quizzService.QuizzSubject.subscribe(
      (quizz: any[]) => {
        this.quizz = quizz;
      }
    );
    console.log(this.quizz);
  }
}
