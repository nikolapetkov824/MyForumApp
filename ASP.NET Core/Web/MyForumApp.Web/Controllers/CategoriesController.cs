namespace MyForumApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly ILogger<CategoriesController> logger;

        public CategoriesController(
            ICategoriesService categoriesService,
            ILogger<CategoriesController> logger)
        {
            this.categoriesService = categoriesService;
            this.logger = logger;
        }

        public IActionResult GetByName(string name)
        {
            var viewModel =
                this.categoriesService.GetByName<CategoryViewModel>(name);

            return this.View(viewModel);
        }
    }
}