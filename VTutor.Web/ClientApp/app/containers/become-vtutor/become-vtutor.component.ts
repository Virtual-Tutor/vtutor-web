import { Component, TemplateRef } from '@angular/core';

import { Tutor } from '../../models/Tutor';
import { TutorsService, TutorInterestEmail } from '../../shared/tutors.service';
import { LoginService } from '../../shared/login.service';
import { SubjectsService } from '../../shared/subjects.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { Grade, Subject, TutorSubject } from '../../models/Tutor';

@Component({
	selector: 'vt-become-vtutor',
	templateUrl: './become-vtutor.component.html',
	styleUrls: ['become-vtutor.component.scss']
})
export class BecomeVTutor {
	modalRef: BsModalRef;
	tutor: TutorInterestEmail;

	constructor(private tutors: TutorsService, private modalService: BsModalService) {
		this.tutor = new TutorInterestEmail();
	}

	public submit(template: TemplateRef<any>) {
		this.tutors.SendInterestEmail(this.tutor).subscribe(x => {
			/** Nothin' to see here, move along **/
			this.modalRef = this.modalService.show(template);
		});

	}


}

