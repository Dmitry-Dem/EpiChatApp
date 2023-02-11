using EpiChatApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EpiChatApp.ViewComponents
{
	public class MenuViewComponent : ViewComponent
	{
		private ApplicationDBContext _dbContext;
		public MenuViewComponent(ApplicationDBContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public IViewComponentResult Invoke()
		{
			var chats = _dbContext.ChatUsers
				.Include(x => x.Chat)
				.Where(x => x.AppUserId == GetUserId())
				.Select(x => x.Chat)
				.ToList();

			return View(chats);
		}
		private string GetUserId()
		{
			return HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
