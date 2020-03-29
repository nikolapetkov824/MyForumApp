using MyForumApp.Data.Models;
using MyForumApp.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForumApp.Web.ViewModels.Replies
{
    public class ReplyViewModel : IMapFrom<Reply>
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public int PostId { get; set; }

        public IEnumerable<IndexReplyViewModel> Replies { get; set; }
    }
}
