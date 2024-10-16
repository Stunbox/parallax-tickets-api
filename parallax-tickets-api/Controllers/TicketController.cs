using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace parallax_tickets_api.Controllers
{
	[Route("/api/ticket")]
	public class TicketController : Controller
	{
		[Authorize]
		[HttpGet]
		public IActionResult GetAll() {
			return Ok();
		}
	}
}
