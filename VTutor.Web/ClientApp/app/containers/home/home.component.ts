import { Component, OnInit, Inject } from '@angular/core';
import { isPlatformBrowser, isPlatformServer } from '@angular/common';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'vt-app-home',
    templateUrl: './home.component.html',
    styleUrls:['./home.component.scss']
})
export class HomeComponent implements OnInit {

    title: string = 'Angular 4.0 Universal & ASP.NET Core 2.0 advanced starter-kit';
	carouselInterval: number;

    // Use "constructor"s only for dependency injection
    constructor(
      public translate: TranslateService
    ) { }

    // Here you want to handle anything with @Input()'s @Output()'s
    // Data retrieval / etc - this is when the Component is "ready" and wired up
	ngOnInit() {
		this.carouselInterval = 0;
		//if (isPlatformBrowser) {
		//	this.carouselInterval = 5000;
		//}
		//else {
		//	this.carouselInterval = 0;
		//}
	}



    public setLanguage(lang) {
        this.translate.use(lang);
    }
}
