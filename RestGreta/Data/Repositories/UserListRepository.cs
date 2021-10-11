using System;
using System.Collections.Generic;
using System.Linq;
using RestGreta.Data.Entities;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;


namespace RestGreta.Data.Repositories
{
    public interface IUserListRepository
    {
        Task Create(UserList userList);
        Task Delete(string id);
        Task<UserList> Get(string id);
        Task<IEnumerable<UserList>> GetAll();
        Task Put(UserList userList);
    }

    public class UserListRepository : IUserListRepository
    {
        internal MongoDBContext db = new MongoDBContext();
        public async Task<IEnumerable<UserList>> GetAll()
        {
            try
            {
                return await db.UserList.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<UserList> Get(string id)
        {
                try
                {
                    FilterDefinition<UserList> filter = Builders<UserList>.Filter.Eq(s => s.Id, new string(id));
                    return await db.UserList.Find(filter).FirstOrDefaultAsync();
                }
                catch
                {

                    return null;
                }

            }
        public async Task Create(UserList userList)
        {
            try
            {
                await db.UserList.InsertOneAsync(userList);

            }
            catch
            {

                throw;
            }
        }
        public async Task Put(UserList userList)
        {
            try
            {
                var filter = Builders<UserList>
                    .Filter
                    .Eq(s => s.Id, userList.Id);
                await db.UserList.ReplaceOneAsync(filter, userList);
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
                var filter = Builders<UserList>.Filter.Eq(s => s.Id, new string(id));
                await db.UserList.DeleteOneAsync(filter);
            }
            catch
            {

                throw;
            }
        }
    }
}
