import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { QuizzService } from './services/Quizz.service';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private quizzService: QuizzService) {}

  ngOnInit() {
    // this.quizzService.emitQuizzSubject();
  }

}
