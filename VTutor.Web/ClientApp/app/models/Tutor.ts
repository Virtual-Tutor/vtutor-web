export class Tutor {
	id: string;
	firstName: string;
	lastName: string;
	phoneNumber: string;
	email: string;
	biography: string;
	education: string;
	languages: string;
	hobbies: string;

	/*
		TODO: images and documents...
	*/

	subjects: TutorSubject[];
}

export class TutorSubject {
	name: Subject;
	subjectGrade: SubjectGrade;
}

export class SubjectGrade {
	name: Grade;
}

export enum Grade {
	Sixth = 6,
	Seventh = 7,
	Eighth = 8,
	Ninth = 9,
	Tenth = 10,
	Eleventh = 11,
	Twelfth = 12
}


export enum Subject {
	Math = "Math",
	English = "English",
	Spanish = "Spanish",
	French = "French",
	Statistics = "Statistics",
	Calculus = "Calculus",
	Trigonometry = "Trigonometry",
	Geometry = "Geometry",
	Algebra = "Algebra",
	Algebra2 = "Algebra 2"
}
