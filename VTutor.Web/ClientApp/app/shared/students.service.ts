import { Injectable } from '@angular/core';

import { Student } from '../models/Student';
import { Http } from '@angular/http';

export class ReferAFriendForm {
	YourName: string;
	YourEmailAddress: string;
	YourFriendsName: string;
	YourFriendsEmailAddress: string;
	TellUsAboutYourFriend: string;
}

@Injectable()
export class StudentsService {

	constructor(private http: Http) {

	}


	public GetLoggedInStudent() {
		return this.http.get('api/students?current=true');
	}

	public ReferAFriend(form: ReferAFriendForm) {
		return this.http.post('api/emails/refer-a-friend', form);
	}

	public SaveStudent(student: Student) {
		if (student.id == null) {
			return this.http.post('api/students', student);
		}
		else {
			return this.http.put('api/students/' + student.id, student);
		}
	}

	public GetAppointments() {
		return this.http.get('api/appointments').map(response => {
			let events = <any[]>response.json();
			return events.map(x => {
				return { startTime: new Date(x.startTime), endTime: new Date(x.endTime), id: x.id, tutor: null };
			})
		});
	}


}
