using EpiChatApp.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EpiChatApp.ViewModels
{
	public class ChatViewModel
	{
		public IFormFile? ChatImage { get; set; }
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; } = null!;
		public ChatType Type { get; set; }
	}
}
