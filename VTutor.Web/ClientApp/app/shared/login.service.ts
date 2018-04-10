import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser, isPlatformServer } from '@angular/common';
import { Router } from '@angular/router'
import { ORIGIN_URL } from './constants/baseurl.constants';
import { Http } from '@angular/http';
import { TransferHttp } from '../../modules/transfer-http/transfer-http';
import { CookieService } from 'ngx-cookie-service';

const VTUTOR_TOKEN = 'vtutor_login_token';

export enum Identity {
	Tutor,
	Student,
	Admin
}

@Injectable()
export class LoginService {

	public Role: string;
	private IdentityCookie: string;

	constructor(
		@Inject(ORIGIN_URL) private baseUrl: string,
		@Inject(PLATFORM_ID) private platformId: Object,
		private http: Http,
		private transferHttp: TransferHttp,
		private router: Router,
		private cookies: CookieService) {
		
	}

	Logout() {
		this.IdentityCookie = null;
		this.cookies.delete(VTUTOR_TOKEN);
	}

	Login(username: string, password: string, path:string) {
		this.http.post(`account/login?email=${username}&password=${password}`, {}).subscribe(result => {
			this.Role = result.json()[0];
			//store the role ...
			this.cookies.set(VTUTOR_TOKEN, this.Role);

			if (path != null) {
				this.router.navigate([path]);
			}
			else {
				if (this.Role == 'Tutors') {
					this.router.navigate(['dashboard']);
				}
				else if (this.Role == 'Students') {
					this.router.navigate(['student-dashboard'])
				}
				else if (this.Role == 'Admin') {
					this.router.navigate(['admin-dashboard']);
				}
			}

			
		})
	}

	public IsLoggedIn() {
		if (isPlatformBrowser(this.platformId)) {
			this.IdentityCookie = this.cookies.get(VTUTOR_TOKEN);
		}

		return this.IdentityCookie != null && this.IdentityCookie != '';
	}

	public Identity(): Identity {
		if (!this.IsLoggedIn()) {
			return null;
		}
		else if(this.IdentityCookie == 'Tutors') {
			return Identity.Tutor;
		}
		else if (this.IdentityCookie == 'Students') {
			return Identity.Student;
		}
		else if (this.IdentityCookie == 'Administrators') {
			return Identity.Admin;
		}
		else {
			return null;
		}
	}

	RegisterTutor(username: string, password: string) {
		return this.http.post(`account/register?email=${username}&password=${password}&registrationType=Tutors`, {});
	}

	RegisterStudent(username: string, password: string) {
		return this.http.post(`account/register?email=${username}&password=${password}&registrationType=Students`, {});
	}

	private GetRoles() {
		this.transferHttp.get(`${this.baseUrl}/account/roles`).subscribe(result => {
			this.Role = result.json()[0];
		})
	}


}
