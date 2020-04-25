namespace MyForumApp.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels;
    using MyForumApp.Web.ViewModels.Home;

    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("Error");
            }

            var viewModel = new IndexViewModel
            {
                Categories =
                    this.categoriesService.GetAll<IndexCategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
