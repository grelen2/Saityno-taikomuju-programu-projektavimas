using RestGreta.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RestGreta.Data.Repositories
{
    
    public interface ICommentRepository
    {
        Task CreateComment(Comment comment);
        Task DeleteComment(string id);
        Task<Comment> GetComment(string id);
        Task<IEnumerable<Comment>> GetAllComments();
        Task PutComment(Comment comment);
    }

    public class CommentRepository : ICommentRepository
    {
        internal MongoDBContext db = new MongoDBContext();
       
       public async Task<IEnumerable<Comment>> GetAllComments()
        {
            try
            {
                return await db.Comment.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<Comment> GetComment(string id)
        {
            try
            {
                FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq(s => s.Id, new string(id));
                return await db.Comment.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }

        }
        public async Task CreateComment(Comment comment)
        {
            try
            {
                await db.Comment.InsertOneAsync(comment);
                
            }
            catch
            {

                throw;
            }
        }
        public async Task PutComment(Comment comment)
        {
            try
            {
                var filter = Builders<Comment>
                    .Filter
                    .Eq(s => s.Id, comment.Id);
                await db.Comment.ReplaceOneAsync(filter, comment);
            }
            catch
            {

                throw;
            }
        }
        public async Task DeleteComment(string id)
        {

            try
            {
                var filter = Builders<Comment>.Filter.Eq(s => s.Id, new string(id));
                await db.Comment.DeleteOneAsync(filter);
            }
            catch
            {

                throw;
            }
        }
    }
}
