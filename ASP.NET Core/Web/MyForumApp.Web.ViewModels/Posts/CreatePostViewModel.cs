namespace MyForumApp.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;
    using MyForumApp.Web.ViewModels.Common;

    public class CreatePostViewModel : ICreatePostViewModel, IMapTo<Post>
    {
        //[PostAlreadyExists]
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [RegularExpression(ModelsConstants.NamesRegex, ErrorMessage = ErrorConstants.NamesAllowedCharactersError)]
        [StringLength(ErrorConstants.MaximumNamesLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumNamesLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumPostDescriptionLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumPostDescriptionLength)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        //public string CategoryName { get; set; }

        //public string UserId { get; set; }

        //public string UserName { get; set; }
    }
}
