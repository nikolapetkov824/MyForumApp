namespace MyForumApp.Web.ViewModels.Categories
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class PostInCategoryViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DaysAgo
        {
            get
            {
                // 1.
                // Parse the date and put in DateTime object.
                DateTime startDate = DateTime.Parse(this.CreatedOn.ToString());

                // 2.
                // Get the current DateTime.
                DateTime now = DateTime.Now;

                // 3.
                // Get the TimeSpan of the difference.
                TimeSpan elapsed = now.Subtract(startDate);

                // 4.
                // Get number of days ago.
                double daysAgo = elapsed.TotalDays;
                return daysAgo.ToString("0");
            }
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content.Length > 300
                    ? content.Substring(0, 300) + "..."
                    : content;
            }
        }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }
    }
}
