using MyForumApp.Data.Models;
using MyForumApp.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForumApp.Web.ViewModels.Replies
{
    public class CreateReplyViewModel : IMapTo<Reply>
    {
        public string Content { get; set; }

        public int CommentId { get; set; }
    }
}
