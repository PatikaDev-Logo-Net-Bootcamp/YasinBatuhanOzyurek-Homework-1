using System;
using System.Collections.Generic;
using System.Text;

namespace GetPostsBackgroundWorker.Dto
{
    // to get posts from json strin
    // Id is primary key so we do not need it
    public class PostDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
