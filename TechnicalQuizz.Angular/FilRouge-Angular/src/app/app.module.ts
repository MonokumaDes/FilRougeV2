import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { QuestionReponseComponent } from './QuestionReponse/QuestionReponse.component';
import { QuizzComponent } from './Quizz/Quizz.component';
import { QuestionReponseService } from './services/QuestionReponse.service';
import { QuizzService } from './services/Quizz.service';


@NgModule({
  declarations: [
    AppComponent
,
    QuestionReponseComponent,
    QuizzComponent
],
  imports: [
    BrowserModule
  ],
  providers: [
    QuizzService,
    QuestionReponseService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
