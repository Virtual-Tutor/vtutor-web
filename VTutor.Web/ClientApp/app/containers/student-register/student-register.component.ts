import { Component } from '@angular/core';
import { StudentsService } from '../../shared/students.service';
import { Student } from '../../models/Student';
import { LoginService } from '../../shared/login.service';

@Component({
    selector: 'vt-student-register',
    templateUrl: './student-register.component.html',
    styleUrls: ['student-register.component.scss']
})
export class StudentRegisterComponent {

	public student: Student;
	public password: string;
	public confirmPassword: string;

	public iAgreeCheck: boolean;

	constructor(private studentsService: StudentsService, private loginService: LoginService) {
		this.student = new Student();
	}

	public onSubmit() {
		this.studentsService.SaveStudent(this.student).subscribe(x => {
			this.loginService.RegisterStudent(this.student.email, this.password).subscribe(r => {
				this.loginService.Login(this.student.email, this.password, null);
			});
		});
	}
}


