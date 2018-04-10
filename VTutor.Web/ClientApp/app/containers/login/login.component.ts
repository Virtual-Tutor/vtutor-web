import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoginService } from '../../shared/login.service';

@Component({
    selector: 'vt-login',
    templateUrl: './login.component.html',
    styleUrls: ['login.component.scss']
})
export class LoginComponent {

	constructor(private loginService:LoginService, private activatedRoute:ActivatedRoute) {

	}

	public email: string;
	public password: string;

	login() {
		let path = this.activatedRoute.snapshot.paramMap.get('path');
		this.loginService.Login(this.email, this.password, path);
	}

}
