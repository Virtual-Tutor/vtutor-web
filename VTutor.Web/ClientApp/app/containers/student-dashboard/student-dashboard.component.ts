import { Component } from '@angular/core';
import { StudentsService } from '../../shared/students.service';
import { Event } from '../../models/Event';

@Component({
	selector: 'vt-student-dashboard',
	templateUrl: './student-dashboard.component.html',
	styleUrls: ['student-dashboard.component.scss']
})
export class StudentDashboardComponent {
	appointments: Event[];
	constructor(private studentService: StudentsService) {

	}

	ngOnInit() {
		this.studentService.GetAppointments()
			.subscribe(result => this.appointments = result);
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
}
