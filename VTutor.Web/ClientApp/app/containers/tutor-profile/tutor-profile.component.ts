import { Component } from '@angular/core';
import { Tutor } from '../../models/Tutor';
import { TutorsService } from '../../shared/tutors.service';

@Component({
	selector: 'vt-tutor-profile',
	templateUrl: './tutor-profile.component.html',
	styleUrls: ['tutor-profile.component.scss']
})
export class TutorProfileComponent {

	public tutor: Tutor;

	constructor(private tutorService:TutorsService) {
		this.tutor = new Tutor();
	}


	ngOnInit() {
		this.tutorService.GetCurrentTutor().subscribe(t => {
			this.tutor = t.json();
		});
	}

	public submit() {
		this.tutorService.SaveTutor(this.tutor).subscribe(t => { });
	}

	public onProfileImageChange(event) {
		let reader = new FileReader();
		if (event.target.files && event.target.files.length > 0) {
			let file = event.target.files[0];
			reader.onload = () => {
				this.tutorService.SaveTutorCertificate(this.tutor.id, { file: reader.result })
					.subscribe(x => { });
			};

			reader.readAsDataURL(file);
			

		}
	}


	public onDocumentFileChange(event) {
		let reader = new FileReader();
		if (event.target.files && event.target.files.length > 0) {
			let file = event.target.files[0];
			reader.onload = () => {
				this.tutorService.SaveProfileImage(this.tutor.id, { file: reader.result })
					.subscribe(x => { });
			};

			reader.readAsDataURL(file);


		}
		
	}


}
