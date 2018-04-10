
import { Component } from '@angular/core';
import { EventsService } from '../../shared/events.service';
import { Event } from '../../models/Event';

@Component({
    selector: 'vt-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['dashboard.component.scss']
})
export class DashboardComponent {

	constructor(private eventService: EventsService) {
		
	}

	ngOnInit() {
		this.events = this.eventService.GetEvents();
	}



	events:Event[];

}

