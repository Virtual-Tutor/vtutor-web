import { Component } from '@angular/core';
import { StudentsService, ReferAFriendForm } from '../../shared/students.service';

@Component({
    selector: 'vt-refer-a-friend',
    templateUrl: './refer-a-friend.component.html',
    styleUrls: ['refer-a-friend.component.scss']
})
export class ReferAFriendComponent {

	public form: ReferAFriendForm

	public constructor(private students: StudentsService) {
		this.form = new ReferAFriendForm();
	}

	public submit() {
		this.students.ReferAFriend(this.form);
	}
}
