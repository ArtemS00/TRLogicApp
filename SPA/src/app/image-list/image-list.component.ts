import { Component } from '@angular/core';
import { ImagesService } from '../../services/images-service';
import { Image } from '../../entities/image';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-image-list',
  templateUrl: './image-list.component.html',
  styleUrls: ['./image-list.component.css']
})
export class ImageListComponent {
  images: {src, id}[] = [];

  constructor(imagesService: ImagesService,
    private _sanitizer: DomSanitizer,
    private _router: Router) {
    imagesService.getAll().subscribe(
      result => {
        this.readImages(result);
      });
  }

  onClick(id): void {
    this._router.navigate([`image/${id}`]);
  }

  readImages(images: Image[]) {
    for (let i = 0; i < images.length; i++) {
      this.images[i] = {
        src: this._sanitizer
          .bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + images[i].preview),
        id: images[i].id
      };
    }
  }
}
