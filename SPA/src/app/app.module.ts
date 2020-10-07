import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { IMAGES_API_URL } from './app-injection-tokens';
import { ImageListComponent } from './image-list/image-list.component';


@NgModule({
  declarations: [
    AppComponent,
    ImageListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: IMAGES_API_URL,
      useValue: environment.IMAGES_API_URL
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
