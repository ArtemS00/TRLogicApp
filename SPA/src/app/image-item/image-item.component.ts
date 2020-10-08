import { Component } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ImagesService } from '../../services/images-service';

@Component({
  selector: 'app-image-item',
  templateUrl: './image-item.component.html',
  styleUrls: ['./image-item.component.css']
})
export class ImageItemComponent {
  image: SafeResourceUrl;

  constructor(route: ActivatedRoute,
    private imagesService: ImagesService,
    private _sanitizer: DomSanitizer,
    router: Router) {
    let id = route.snapshot.params['id'];
    imagesService.get(id).subscribe(result => {
      this.image = this._sanitizer
        .bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + result.data);
      if (!this.image)
        router.navigate(['']);
    },
      error => {
        router.navigate(['']);
      })
  }

}
