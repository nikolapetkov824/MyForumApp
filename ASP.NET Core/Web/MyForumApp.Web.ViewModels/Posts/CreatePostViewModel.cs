﻿namespace MyForumApp.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;
    using MyForumApp.Web.ViewModels.Common;

    public class CreatePostViewModel : IMapTo<Post>
    {
        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [StringLength(
            ErrorConstants.MaximumPostDescriptionLength,
            ErrorMessage = ErrorConstants.StringLengthErrorMessage,
            MinimumLength = ErrorConstants.MinimumPostDescriptionLength)]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
