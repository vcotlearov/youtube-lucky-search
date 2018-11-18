import { Component, OnInit, Inject  } from '@angular/core';
import { DomSanitizer, SafeUrl, SafeResourceUrl } from "@angular/platform-browser";
import { HttpClient, HttpParams } from '@angular/common/http';

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
        { value: 'OMG', viewValue: 'OMG (>5M views)' }
    ];

    chosenRating: string;
    ratings: VideoOption[] = [
        { value: 'Disaster', viewValue: 'Disaster (<40%)' },
        { value: 'Soso', viewValue: 'So-so (40-70%)' },
        { value: 'Good', viewValue: 'Good (70-90%)' },
        { value: 'Awesome', viewValue: 'Awesome (>90%)' }
    ];

    selectedPopularity: number;
    selectedRating: number;
   

    http: HttpClient;
    baseUrl: string;

    constructor(private sanitizer: DomSanitizer, http: HttpClient, @Inject('BASE_URL') baseUrl: string)
    {
        this.videoLoaded = false;
        this.http = http;
        this.baseUrl = baseUrl;
    }

    videoLoaded = true;
    videoTemplate = "https://www.youtube.com/embed/";
    completeURL: SafeResourceUrl;

    public GetVideo()
    {
        let params = new HttpParams();
        params = params.append('videoLength', this.chosenLength);
        params = params.append('views', this.chosenPopularity);
        params = params.append('rating', this.chosenRating);

        this.videoLoaded = false;
        this.http.get<YoutubeVideo>(this.baseUrl + 'api/SampleData/YoutubeLuckySearch', { params: params }).subscribe(result => {
            this.completeURL = this.sanitizer.bypassSecurityTrustResourceUrl(this.videoTemplate + result.id);
            this.selectedPopularity = result.statistics.viewCount;
            this.selectedRating = result.statistics.rating;
            this.videoLoaded = true;
        }, error => console.error(error));
    }
}

interface YoutubeVideo {
    id: string;
    statistics: YoutubeStatistics;
}
interface YoutubeStatistics {
    viewCount: number;
    rating: number;
}

interface LuckySetup {
    videoLength: string;
    views: string;
    rating: string;
}

