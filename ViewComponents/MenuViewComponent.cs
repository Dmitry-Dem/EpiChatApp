using EpiChatApp.Data;
using Microsoft.AspNetCore.Mvc;
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
			//string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			
			return View();
		}
	}
}
