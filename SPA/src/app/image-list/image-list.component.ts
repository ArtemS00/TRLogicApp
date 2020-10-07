import { Component } from '@angular/core';
import { ImagesService } from '../../services/images-service';
import { Image } from '../../entities/image';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-image-list',
  templateUrl: './image-list.component.html',
  styleUrls: ['./image-list.component.css']
})
export class ImageListComponent {
  images = [];

  constructor(
    private imagesService: ImagesService,
    private _sanitizer: DomSanitizer) {
    imagesService.getAll().subscribe(
      result => {
        this.readImages(result);
      });
  }

  readImages(images: Image[]) {
    for (let i = 0; i < images.length; i++) {
      this.images[i] = this._sanitizer
        .bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + images[i].preview);
    }
  }
}
