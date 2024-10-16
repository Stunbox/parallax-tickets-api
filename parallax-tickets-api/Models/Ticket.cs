namespace parallax_tickets_api.Models
{
	public class Ticket
	{
		public int id { get; set; }
		public string title { get; set; }
		public string? description { get; set; }
		public string status { get; set; }
		public DateTime? created_at { get; set; }
		public DateTime? updated_at { get; set;}
	}
}
