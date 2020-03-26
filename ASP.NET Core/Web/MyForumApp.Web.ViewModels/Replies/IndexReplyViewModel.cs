using MyForumApp.Data.Models;
using MyForumApp.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForumApp.Web.ViewModels.Replies
{
    public class IndexReplyViewModel : IMapFrom<Reply>
    {
        public int CommentId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
