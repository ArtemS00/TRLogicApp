import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { IMAGES_API_URL } from '../app/app-injection-tokens';
import { Observable } from 'rxjs';
import { Image } from '../entities/image';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {
  constructor(
    private http: HttpClient,
    @Inject(IMAGES_API_URL) private imagesUrl: string) {
  }

  getAll() : Observable<Image[]> {
    return this.http.get<Image[]>(this.imagesUrl);
  }

  get(id:number): Observable<Image> {
    return this.http.get<Image>(this.imagesUrl + id);
  }
}
