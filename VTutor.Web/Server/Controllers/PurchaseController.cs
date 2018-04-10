using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Braintree;
using Microsoft.AspNetCore.Identity;
using VTutor.Model;
using System.Runtime.Serialization;

namespace VTutor.Web.Controllers
{

	public class ClientToken
	{
		public string token { get; set; }

		public ClientToken(string token)
		{
			this.token = token;
		}
	}

	public class Nonce
	{
		public string nonce { get; set; }
		public decimal chargeAmount { get; set; }

		public Nonce(string nonce)
		{
			this.nonce = nonce;
			this.chargeAmount = chargeAmount;
		}
	}

	[DataContract]
	public class BadRequestResult : Result<Transaction>
	{
		private string errorMessage;
		public BadRequestResult(string errorMessage)
		{
			this.errorMessage = errorMessage;
		}

		public static BadRequestResult Create(string errorMessage)
		{
			return new BadRequestResult(errorMessage);
		}

		public CreditCardVerification CreditCardVerification => throw new NotImplementedException();

		public Transaction Transaction => throw new NotImplementedException();

		public Subscription Subscription => throw new NotImplementedException();

		[DataMember]
		public ValidationErrors Errors
		{
			get
			{
				var errors = new ValidationErrors();

				///errors.AddError("Promo Code", new ValidationError("", "", "test error"));
				return errors;
			}
			
		}

		public Dictionary<string, string> Parameters => throw new NotImplementedException();

		[DataMember]
		public string Message => this.errorMessage;

		public Transaction Target => throw new NotImplementedException();

		
		public bool IsSuccess()
		{
			return false;
		}
	}


	[Produces("application/json")]
	[Route("api/Purchase")]
	public class PurchaseController : Controller
	{
		/// <summary>
		/// User manager - attached to application DB context
		/// </summary>
		protected UserManager<ApplicationUser> UserManager { get; set; }
		private readonly VTutorContext _context;
		private readonly IConfiguration _configuration;

		public PurchaseController(IConfiguration configuration, VTutorContext context, UserManager<ApplicationUser> userManager)
		{
			_configuration = configuration;
			_context = context;
			this.UserManager = userManager;
		}

		[HttpGet]
		[Route("getclienttoken")]
		public async Task<IActionResult> GetClientToken()
		{
			var gateway = new BraintreeGateway
			{
				Environment = Braintree.Environment.SANDBOX,
				MerchantId = "z6qjm7h62x79j3n9",
				PublicKey = "nrp3h97h7brd6s99",
				PrivateKey = "c28a54a9c4fed5e4058197d2787380c9"
			};

			var clientToken = gateway.ClientToken.Generate();
			ClientToken ct = new ClientToken(clientToken);
			return Ok(ct);
		}


		public class Nonce
		{
			public string nonce { get; set; }
			public decimal chargeAmount { get; set; }

			public Nonce(string nonce)
			{
				this.nonce = nonce;
				this.chargeAmount = chargeAmount;
			}
		}

		[HttpPost]
		[Route("createPurchase")]
		public async Task<IActionResult> PurchaseSession([FromQuery]DateTime startDate, [FromQuery]string promo, [FromBody]Nonce nonce)
		{
			var user = await this.UserManager.GetUserAsync(this.User);

			if (user == null)
			{
				return StatusCode(401);
			}

			var student = _context.Students.Where(s => s.Email == user.Email).FirstOrDefault();

			if (student == null)
			{
				return BadRequest("Tutors are not allowed to request sessions.");
			}

			var gateway = new BraintreeGateway
			{
				Environment = Braintree.Environment.SANDBOX,
				MerchantId = "z6qjm7h62x79j3n9",
				PublicKey = "nrp3h97h7brd6s99",
				PrivateKey = "c28a54a9c4fed5e4058197d2787380c9"
			};


			var promoCode = _context.PromoCodes.Where(p => p.Name == promo).FirstOrDefault();
			decimal price = nonce.chargeAmount;

			if (promo != null && promo != "null" && promoCode == null)
			{
				//The passed in code is invalid.
				return Ok(BadRequestResult.Create("The promo you have attempted to use does not exist or is invalid"));
			}
			else if (promoCode != null)
			{
				int usageCount = _context.PromoCodeUsages.Count(x => x.PromoCode.Id == promoCode.Id);
				int personalUsageCount = _context.PromoCodeUsages.Count(x => x.PromoCode.Id == promoCode.Id && x.Student.Id == student.Id);

				if (usageCount >= promoCode.TotalUsageAmount)
				{
					//promo has been used too many times
					return Ok(BadRequestResult.Create("This promo is no longer active."));
				}

				if (personalUsageCount >= promoCode.IndividualUsageAmount)
				{
					return Ok(BadRequestResult.Create("You have used this promo too many times."));
				}

				if (promoCode != null && promoCode.DiscountAmount != 0)
				{
					//discount amount is a percentage from 0 to 100
					price = price * (decimal)promoCode.DiscountAmount / 100;
				}
			}

			

			var request = new TransactionRequest
			{
				Amount = price,
				PaymentMethodNonce = nonce.nonce,
				Options = new TransactionOptionsRequest
				{
					SubmitForSettlement = true
				}
			};

			Result<Transaction> result = await gateway.Transaction.SaleAsync(request);


			if (result.IsSuccess())
			{
				_context.Appointments.Add(new Appointment()
				{
					StartTime = startDate,
					EndTime = startDate.AddHours(1),
					Student = student,
					Tutor = null
				});

				if (promoCode != null)
				{
					_context.PromoCodeUsages.Add(new PromoCodeUsage()
					{
						PromoCode = promoCode,
						Student = student,
						UsedDate = DateTime.Now
					});
				}

				await _context.SaveChangesAsync();

				//session is booked WOOT
			}

			return Ok(result);
		}


	}
}
