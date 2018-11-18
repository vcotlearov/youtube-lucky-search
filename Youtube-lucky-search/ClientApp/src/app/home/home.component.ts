import { Component, OnInit  } from '@angular/core';
import { DomSanitizer, SafeUrl, SafeResourceUrl } from "@angular/platform-browser";

export interface VideoOption {
    value: string;
    viewValue: string;
}


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    ngOnInit(): void {
        
    }
    chosenKeyword: string;

    chosenLength: string;
    lengths: VideoOption[] = [
        { value: 'Short', viewValue: 'Short (<4 mins)' },
        { value: 'Medium', viewValue: 'Medium (4-20 mins)' },
        { value: 'Long', viewValue: 'Long (>20 mins)' }
    ];

    chosenPopularity: string;
    popularities: VideoOption[] = [
        { value: 'Private', viewValue: 'Private' },
        { value: 'Niche', viewValue: 'Niche (>5k views)' },
        { value: 'Popular', viewValue: 'Popular (>50k views)' },
        { value: 'Viral', viewValue: 'Viral (>500k views)' },
        { value: 'OMG', viewValue: 'OMG (>1M views)' }
    ];

    chosenRating: string;
    ratings: VideoOption[] = [
        { value: 'Disaster', viewValue: 'Disaster (<40%)' },
        { value: 'Soso', viewValue: 'So-so (40-70%)' },
        { value: 'Good', viewValue: 'Good (70-90%)' },
        { value: 'Awesome', viewValue: 'Awesome (>90%)' }
    ];


    videoLoaded = true;
    videoTemplate = "https://www.youtube.com/embed/";
    completeURL: SafeResourceUrl;
    videoID: string;

    constructor(private sanitizer: DomSanitizer)
    {
        this.videoLoaded = false;
    }

    public GetVideo()
    {
        this.videoID = "YE7VzlLtp-4";
        this.completeURL = this.sanitizer.bypassSecurityTrustResourceUrl(this.videoTemplate + this.videoID);

        this.videoLoaded = !this.videoLoaded;
    }
}
