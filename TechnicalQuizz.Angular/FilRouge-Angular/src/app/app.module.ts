import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { QuestionReponseComponent } from './QuestionReponse/QuestionReponse.component';
import { QuizzComponent } from './Quizz/Quizz.component';
import { QuestionReponseService } from './services/QuestionReponse.service';
import { QuizzService } from './services/Quizz.service';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';


const appRoutes: Routes = [
  { path: 'Quizz', component: QuizzComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    QuestionReponseComponent,
    QuizzComponent
],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule
  ],
  providers: [
    QuizzService,
    QuestionReponseService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
