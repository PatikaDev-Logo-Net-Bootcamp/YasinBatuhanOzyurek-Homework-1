using First.App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace First.App.Business.Abstract
{
    public interface IPostService
    {
        IEnumerable<Post> GetAllPosts();
        void AddPosts(IEnumerable<Post> posts);
    }
}
