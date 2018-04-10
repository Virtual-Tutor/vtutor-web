using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTutor.Email.TemplateModels
{
	public class ReferAFriendForm
	{
		string YourName { get; set; }
		string YourEmailAddress { get; set; }
		string YourFriendsName { get; set; }
		string YourFriendsEmailAddress { get; set; }
		string TellUsAboutYourFriend { get; set; }
	}
}
