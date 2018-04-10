import { Component, Input, TemplateRef } from '@angular/core';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ModalModule } from 'ngx-bootstrap/modal';
import { Event } from '../../models/Event';
import { EventsService } from '../../shared/events.service';

class Day {
	Date:Date
    DayOfWeek: number;
    DayOfMonth: number;
    ActiveDay: boolean;
}

@Component({
    selector: 'vt-calendar',
    templateUrl: './calendar.component.html',
    styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent {
	modalRef: BsModalRef;

    events: Event[];

    days: Day[];

	calendarDate: Date;

    currentYear: number;
    currentMonth: string;
	currentDayOfMonth: number;

	editingDate: Date;

	constructor(private eventsService:EventsService, private modalService: BsModalService) {
        this.days = [];
    }

	ngOnInit() {

		this.events = [];
		this.calendarDate = new Date();

		this.eventsService.GetAvailableBlocks().subscribe(events => {
			events.forEach(e => this.events.push(e));
		});

		this.setupCalendar();
	}


	setupCalendar() {
		this.days.length = 0;

		//let today = new Date();
		this.currentMonth = this.months[this.calendarDate.getMonth()];
		this.currentYear = this.calendarDate.getFullYear();
		this.currentDayOfMonth = this.calendarDate.getDate();

		for (var i = 1; i <= this.daysInMonth(this.calendarDate.getMonth(), this.calendarDate.getFullYear()); i++) {
			let date = new Date(this.calendarDate.getFullYear(), this.calendarDate.getMonth(), i);
			let dayOfMonth = date.getDate();
			let dayOfWeek = date.getDay();

			let isToday = dayOfMonth == this.calendarDate.getDate();
			this.days.push({ Date: date, DayOfWeek: dayOfWeek, DayOfMonth: dayOfMonth, ActiveDay: isToday });
		}
	}


    prevMonth() {
		this.calendarDate.setMonth(this.calendarDate.getMonth() - 1);
		this.setupCalendar();
    }

    nextMonth() {
		this.calendarDate.setMonth(this.calendarDate.getMonth() + 1);
		this.setupCalendar();
    }

	slotSelected(slot: Date) {
		return this.events.filter(x => x.startTime.valueOf() == slot.valueOf()).length > 0;
	}

	getEventForDate(slot: Date) {
		return this.events.filter(x => x.startTime.valueOf() == slot.valueOf())[0];
	}


	addOrRemoveSlot(slot: Date) {
		if (this.slotSelected(slot)) {
			this.eventsService.DeleteAvailableBlock(this.getEventForDate(slot)).subscribe(response => {
				this.events = this.events.filter(x => x.startTime.valueOf() != slot.valueOf());
			});
			
		}
		else {
			let endTime = new Date(slot);
			endTime.setHours(slot.getHours() + 1);
			let event = { startTime: slot, endTime: endTime, tutor:null };
			this.eventsService.AddAvailableBlock(event).subscribe(response => {
				this.events.push(event);
			});
			
		}
	}

	getAvailableSlots(date:Date) {
		return this.eventsService.GetAllowedTimeSlots(date);
	}

	formatTime(date: Date) {
		if (date.getHours() < 13) {
			if (date.getMinutes() == 0) {
				return date.getHours() + ':00 AM';
			}
			else {
				return date.getHours() + ':' + date.getMinutes() + ' AM';
			}
		}
		else {
			if (date.getMinutes() == 0) {
				return (date.getHours() - 12) + ':00 PM';
			}
			else {
				return (date.getHours() - 12) + ':' + date.getMinutes() + ' PM';
			}
		}	
	}

	formatEvent(event: Event) {
		return this.formatTime(event.startTime) + ' - ' + this.formatTime(event.endTime);
	}

	editDay(template: TemplateRef<any>, date: Date) {
		this.editingDate = date;
		this.modalRef = this.modalService.show(template);
	}

	getEventText(event: Event) {
		return this.formatEvent(event);
	}

	getEventsOnDay(dayOfMonth: number) {
		return this.events.filter(x => x.startTime.getDate() == dayOfMonth && x.startTime.getMonth() == this.calendarDate.getMonth());
	}

    mondays() {
        return this.days.filter((x) => x.DayOfWeek == 1);
	}

	firstMondayIsInSecondWeek() {
		var firstDay = this.mondays().sort((x, y) => x.DayOfMonth - y.DayOfMonth)[0];
		return firstDay.DayOfMonth > firstDay.DayOfWeek;
	}

    tuesdays() {
        return this.days.filter((x) => x.DayOfWeek == 2);
	}

	firstTuesdayIsInSecondWeek() {
		var firstDay = this.tuesdays().sort((x, y) => x.DayOfMonth - y.DayOfMonth)[0];
		return firstDay.DayOfMonth > firstDay.DayOfWeek;
	}

    wednesdays() {
        return this.days.filter((x) => x.DayOfWeek == 3);
	}

	firstWednesdayIsInSecondWeek() {
		var firstDay = this.wednesdays().sort((x, y) => x.DayOfMonth - y.DayOfMonth)[0];
		return firstDay.DayOfMonth > firstDay.DayOfWeek;
	}

    thursdays() {
        return this.days.filter((x) => x.DayOfWeek == 4);
	}

	firstThursdayIsInSecondWeek() {
		var firstDay = this.thursdays().sort((x, y) => x.DayOfMonth - y.DayOfMonth)[0];
		return firstDay.DayOfMonth > firstDay.DayOfWeek;
	}

    fridays() {
        return this.days.filter((x) => x.DayOfWeek == 5);
	}

	firstFridayIsInSecondWeek() {
		var firstDay = this.fridays().sort((x, y) => x.DayOfMonth - y.DayOfMonth)[0];
		return firstDay.DayOfMonth > firstDay.DayOfWeek;
	}

    saturdays() {
        return this.days.filter((x) => x.DayOfWeek == 6);
    }

	firstSaturdayIsInSecondWeek() {
		var firstDay = this.saturdays().sort((x, y) => x.DayOfMonth - y.DayOfMonth)[0];
		return firstDay.DayOfMonth > firstDay.DayOfWeek;
	}

    sundays() {
        return this.days.filter((x) => x.DayOfWeek == 0);
    }

    daysInMonth(month, year) {
      return new Date(year, month + 1, 0).getDate();
    }

    months: string[] = [
        "JANUARY",
        "FEBRUARY",
        "MARCH",
        "APRIL",
        "MAY",
        "JUNE",
        "JULY",
        "AUGUST",
        "SEPTEMBER",
        "OCTOBER",
        "NOVEMBER",
        "DECEMBER"
    ];


}
