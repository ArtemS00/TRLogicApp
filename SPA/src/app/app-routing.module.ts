import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ImageItemComponent } from './image-item/image-item.component';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { ImageListComponent } from './image-list/image-list.component';

const routes: Routes = [
  { path: '', component: ImageListComponent, pathMatch: 'full' },
  { path: 'image/:id', component: ImageItemComponent }]


@NgModule({
  imports: [BrowserModule, RouterModule.forRoot(routes)],
  declarations: [ImageItemComponent],
  bootstrap: [AppComponent]
})
export class AppRoutingModule { }
