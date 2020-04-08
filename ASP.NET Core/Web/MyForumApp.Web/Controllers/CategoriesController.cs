﻿namespace MyForumApp.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private const int ItemsPerPage = 5;

        private readonly ICategoriesService categoriesService;
        private readonly IPostsService postsService;

        public CategoriesController(
            ICategoriesService categoriesService,
            IPostsService postsService)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
        }

        public IActionResult GetByName(string name, int page = 1)
        {
            var viewModel =
                this.categoriesService.GetByName<CategoryViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.ForumPosts = this.postsService.GetByCategoryId<PostInCategoryViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage);

            var count = this.postsService.GetCountByCategoryId(viewModel.Id);
            viewModel.PageCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (viewModel.PageCount == 0)
            {
                viewModel.PageCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
