﻿namespace MyForumApp.Web.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Categories;

    [Authorize]
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

        /// <summary>
        /// TO DO: Sorting before Pagination.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult GetByName(string name, int page = 1, string sortBy = null)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("Error");
            }

            var viewModel =
                this.categoriesService.GetByName<CategoryViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.ForumPosts = this.postsService.GetByCategoryId<PostInCategoryViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage, sortBy);
            viewModel.ForumPosts = sortBy switch
            {
                "Date" => viewModel.ForumPosts.OrderByDescending(x => x.CreatedOn),
                "Comments" => viewModel.ForumPosts.OrderByDescending(x => x.CommentsCount),
                _ => viewModel.ForumPosts.OrderBy(x => x.Id),
            };

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
