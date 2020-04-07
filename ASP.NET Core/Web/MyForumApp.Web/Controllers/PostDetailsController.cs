﻿namespace MyForumApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Data;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Posts;
    using MyForumApp.Web.ViewModels.Replies;

    public class PostDetailsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public PostDetailsController(
            IPostsService postsService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            this.postsService = postsService;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public IActionResult Details(int postId)
        {
            var post = this.postsService.GetById<PostViewModel>(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }
    }
}