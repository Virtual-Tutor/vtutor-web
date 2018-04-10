import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { Tutor } from '../models/Tutor';

@Injectable()
export class AdminService {
	constructor(private http:Http) {

	}

	getUnapprovedTutors() {
		return this.http.get(`api/tutors/`).map(response => {
			return response.json();
		});
	}

	deleteTutor(tutor:Tutor) {
		return this.http.delete(`api/tutors/${tutor.id}`).subscribe(response => { });
	}
}
