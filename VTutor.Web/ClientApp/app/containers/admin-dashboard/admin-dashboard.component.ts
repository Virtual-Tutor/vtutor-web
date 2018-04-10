
import { Component } from '@angular/core';
import { AdminService } from '../../shared/admin.service';
import { Http } from '@angular/http';
import { Tutor } from '../../models/Tutor';

@Component({
    selector: 'vt-admin-dashboard',
    templateUrl: './admin-dashboard.component.html',
    styleUrls: ['admin-dashboard.component.scss']
})
export class AdminDashboardComponent{
	constructor(private admin: AdminService) {
    
    }
	tutors: Tutor[];
    ngOnInit() {
		this.getUsers();
	}

	getUsers() {
		this.admin.getUnapprovedTutors().subscribe(tutors => {
			this.tutors = tutors;
		});
	}

	accept(tutor:Tutor) {

	}

	reject(tutor: Tutor) {
		this.tutors = this.tutors.filter(x => x != tutor);
		this.admin.deleteTutor(tutor);
	}


}
