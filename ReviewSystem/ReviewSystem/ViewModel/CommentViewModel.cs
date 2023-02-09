using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewSystem.ViewModel
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int ArticleId { get; set; }
        public string CommentDescription { get; set; }
        public int Rating { get; set; }
        public DateTime CommentedOn { get; set; }

    }
}