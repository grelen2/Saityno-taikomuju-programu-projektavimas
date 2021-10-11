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
        Task Create(Comment comment);
        Task Delete(string id);
        Task<Comment> Get(string id);
        Task<IEnumerable<Comment>> GetAll();
        Task Put(Comment comment);
    }

    public class CommentRepository : ICommentRepository
    {
        internal MongoDBContext db = new MongoDBContext();
       
       public async Task<IEnumerable<Comment>> GetAll()
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
        public async Task<Comment> Get(string id)
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
        public async Task Create(Comment comment)
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
        public async Task Put(Comment comment)
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
        public async Task Delete(string id)
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
