import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { environment } from '../environments/environment';
import { IMAGES_API_URL } from './app-injection-tokens';
import { AppComponent } from './app.component';
import { ImageItemComponent } from './image-item/image-item.component';
import { ImageListComponent } from './image-list/image-list.component';
import { AppRoutingModule } from './app-routing.module';


@NgModule({
  declarations: [
    AppComponent,
    ImageListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule,
    AppRoutingModule,
  ],
  providers: [
    {
      provide: IMAGES_API_URL,
      useValue: environment.IMAGES_API_URL
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
