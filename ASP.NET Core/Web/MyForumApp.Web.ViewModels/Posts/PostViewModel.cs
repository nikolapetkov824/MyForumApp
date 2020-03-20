namespace MyForumApp.Web.ViewModels.Posts
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Ganss.XSS;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class PostViewModel : IMapFrom<Post>, IMapTo<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);

        public string UserUserName { get; set; }

        public int VotesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(x => x.VotesCount, options =>
                {
                    options.MapFrom(p => p.Votes.Sum(v => (int)v.VoteType));
                });
        }
    }
}
