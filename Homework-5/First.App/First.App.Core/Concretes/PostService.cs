using First.App.Business.Abstract;
using First.App.DataAccess.EntityFramework.Repository.Abstracts;
using First.App.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace First.App.Business.Concretes
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> repository;
        private readonly IUnitOfWork unitOfWork;

        public PostService(IRepository<Post> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void AddPosts(IEnumerable<Post> posts)
        {
            repository.AddRange(posts);
            unitOfWork.Commit();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return repository.Get().ToList();
        }
    }
}
