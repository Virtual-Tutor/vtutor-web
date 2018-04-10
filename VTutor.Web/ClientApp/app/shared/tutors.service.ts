import { Injectable } from '@angular/core';

import { Tutor } from '../models/Tutor';
import { Http } from '@angular/http';


export class TutorInterestEmail{
	name: string;
	emailAddress: string;
	grade: string;
	subject: string;
	whyVTutor: string;

}

@Injectable()
export class TutorsService {

	constructor(private http: Http) {

	}


	public SendInterestEmail(tutor:TutorInterestEmail) {
		return this.http.post('api/emails/tutor-interest', tutor);
	}

	public SaveTutor(tutor: Tutor) {
		if (tutor.id) {
			return this.http.put('api/tutors/' + tutor.id, tutor);
		}
		else {
			return this.http.post('api/tutors', tutor)
		}
	}

	public SaveTutorCertificate(tutor_id: string, file: any) {
		return this.http.post(`api/tutors/${tutor_id}/certification`, file);
	}

	public SaveProfileImage(tutor_id: string, file:any) {
		return this.http.post(`api/tutors/${tutor_id}/profileImage`, file);
	}

	public GetCurrentTutor() {
		return this.http.get('api/tutors?currentTutor=true');
	}


}
