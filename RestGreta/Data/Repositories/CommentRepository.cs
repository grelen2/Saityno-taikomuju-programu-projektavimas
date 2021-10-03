using RestGreta.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Data.Repositories
{
    
    public interface ICommentRepository
    {
        Task<Comment> Create(Comment comment);
        Task Delete(Comment comment);
        Task<Comment> Get(int id);
        Task<IEnumerable<Comment>> GetAll();
        Task<Comment> Put(Comment comment);
    }

    public class CommentRepository : ICommentRepository
    {
        public async Task<IEnumerable<Comment>> GetAll()
        {
            return new List<Comment>
            {
                new Comment()
                {
                    UserName = "Pitonas",
                    CommentText = "Naujas blynu receptas buvo labai geras",
                    CreationTimeUtc = DateTime.UtcNow
                },
                new Comment()
                {
                    UserName = "Nusivylusi mamyte",
                    CommentText = "Deja, bet balandeliu isvirti nepavyko taip, kaip buvo pateikta aprasyme",
                    CreationTimeUtc = DateTime.UtcNow
                }
            };
        }
        public async Task<Comment> Get(int id)
        {
            return new Comment()
            {
                UserName = "Nusivylusi mamyte",
                CommentText = "Deja, bet balandeliu isvirti nepavyko taip, kaip buvo pateikta aprasyme",
                CreationTimeUtc = DateTime.UtcNow
            };

        }
        public async Task<Comment> Create(Comment comment)
        {
            return new Comment()
            {
                UserName = "Nusivylusi mamyte",
                CommentText = "Deja, bet balandeliu isvirti nepavyko taip, kaip buvo pateikta aprasyme",
                CreationTimeUtc = DateTime.UtcNow
            };
        }
        public async Task<Comment> Put(Comment comment)
        {
            return new Comment()
            {
                UserName = "Nusivylusi mamyte",
                CommentText = "Deja, bet balandeliu isvirti nepavyko taip, kaip buvo pateikta aprasyme",
                CreationTimeUtc = DateTime.UtcNow
            };
        }
        public async Task Delete(Comment comment)
        {
        }
    }
}
