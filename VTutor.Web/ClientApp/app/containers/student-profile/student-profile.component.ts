import { Component } from '@angular/core';
import { Student } from '../../models/Student';
import { StudentsService } from '../../shared/students.service';

@Component({
	selector: 'vt-student-profile',
	templateUrl: './student-profile.component.html',
	styleUrls: ['student-profile.component.scss']
})
export class StudentProfileComponent {

	public student: Student;

	constructor(private studentService:StudentsService) {
		this.student = new Student();
	}

	ngOnInit() {
		this.studentService.GetLoggedInStudent().subscribe(student => {
			this.student = student.json();
		});
	}

	save() {
		this.studentService.SaveStudent(this.student).subscribe(x => { });
	}
}


