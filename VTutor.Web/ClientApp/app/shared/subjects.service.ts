import { Injectable } from '@angular/core';
import { Grade, Subject, TutorSubject } from '../models/Tutor';

@Injectable()
export class SubjectsService {

	constructor() {


	}

	//Hard coded set of values based on whats available
	//someday this may hit the database.
	public GetAllAvailableSubjects() : TutorSubject[] {
		return [
			//grade six...
			{ subjectGrade: { name: Grade.Sixth }, name: Subject.English },
			{ subjectGrade: { name: Grade.Sixth }, name: Subject.Spanish },
			{ subjectGrade: { name: Grade.Sixth }, name: Subject.French },
			{ subjectGrade: { name: Grade.Sixth }, name: Subject.Math },
			//grade seven
			{ subjectGrade: { name: Grade.Seventh }, name: Subject.English },
			{ subjectGrade: { name: Grade.Seventh }, name: Subject.Spanish },
			{ subjectGrade: { name: Grade.Seventh }, name: Subject.French },
			{ subjectGrade: { name: Grade.Seventh }, name: Subject.Math },
			//grade eight
			{ subjectGrade: { name: Grade.Eighth }, name: Subject.English },
			{ subjectGrade: { name: Grade.Eighth }, name: Subject.Spanish },
			{ subjectGrade: { name: Grade.Eighth }, name: Subject.French },
			{ subjectGrade: { name: Grade.Eighth }, name: Subject.Math },
			//grade nine
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.Statistics },
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.Calculus },
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.Trigonometry },
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.Geometry },
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.Algebra },
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.Algebra2 },
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.English },
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.Spanish },
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.French },
			{ subjectGrade: { name: Grade.Ninth }, name: Subject.Math },
			//grade ten
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.Statistics },
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.Calculus },
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.Trigonometry },
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.Geometry },
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.Algebra },
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.Algebra2 },
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.English },
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.Spanish },
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.French },
			{ subjectGrade: { name: Grade.Tenth }, name: Subject.Math },
							  
			//grade eleven	  
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.Statistics },
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.Calculus },
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.Trigonometry },
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.Geometry },
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.Algebra },
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.Algebra2 },
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.English },
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.Spanish },
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.French },
			{ subjectGrade: { name: Grade.Eleventh }, name: Subject.Math },
							  
			//grade twelve	  
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.Statistics },
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.Calculus },
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.Trigonometry },
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.Geometry },
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.Algebra },
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.Algebra2 },
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.English },
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.Spanish },
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.French },
			{ subjectGrade: { name: Grade.Twelfth }, name: Subject.Math }
		];
	}






}
