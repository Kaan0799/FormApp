using Bussiness.Abstract;
using Bussiness.Concrate;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using FormApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormApp.Controllers

{

    public class FormController : Controller
    {
		private IFormService _formService;

        public FormController(IFormService formService)
        {
            this._formService = formService;
        }
        [Authorize]
        public IActionResult Index()
		{
            var claim = HttpContext.User.Claims.First(c => c.Type == "Id");
            var userId = claim.Value;
            var data = _formService.GetAllForm(Convert.ToInt32(userId));
            return View(data);
		}
		[HttpGet]
        [Authorize]
        public IActionResult Addform()
		{

			return View();
		}
		[HttpPost]
        [Authorize]
        public IActionResult Addform(AddFormViewModel p)
		{
			var questions = new List<Entity.Concrate.Question>();
			foreach (var item in p.Questions)
			{
				questions.Add(new Entity.Concrate.Question() { QuestionText = item.QuestionText, FieldType = item.FieldType, IsRequired = item.IsRequired });
			}
            p.CreationDate = DateTime.Now;
            var claim = HttpContext.User.Claims.First(c => c.Type == "Id");
            var userId = claim.Value;
            _formService.AddForm(p.FormName,p.Description,questions, Convert.ToInt32(userId));
			return RedirectToAction("Index", "Form");
		}
		public IActionResult Show(int Id)
        {
            var data = _formService.GetForm(Id);
            var claim = HttpContext.User.Claims.First(c => c.Type == "Id");
            var userId = claim.Value;
            if (data.User.UserId!= Convert.ToInt32(userId))
            {
                TempData["Error"] = "Bu sayfayı görüntülemek için yetkiniz yok.";
                return View(data);
            }
            else
            {
                return View(data);
            }
        }
    }
}
