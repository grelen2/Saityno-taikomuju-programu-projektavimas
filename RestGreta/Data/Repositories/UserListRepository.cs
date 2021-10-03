using System;
using System.Collections.Generic;
using System.Linq;
using RestGreta.Data.Entities;
using System.Threading.Tasks;


namespace RestGreta.Data.Repositories
{
    public interface IUserListRepository
    {
        Task<UserList> Create(UserList userList);
        Task Delete(UserList userList);
        Task<UserList> Get(int id);
        Task<IEnumerable<UserList>> GetAll();
        Task<UserList> Put(UserList userList);
    }

    public class UserListRepository : IUserListRepository
    {
        public async Task<IEnumerable<UserList>> GetAll()
        {
            return new List<UserList>
            {
                new UserList()
                {

                    UserName = "Marijukasss5",
                    Name = "Marija",
                    Surname = "Leonaviciute",
                    Address = "Kaunas, Kauno gatve 3",
                    CreationTimeUtc = DateTime.UtcNow
                },
                new UserList()
                {
                    UserName = "Ona123",
                    Name = "Ona",
                    Surname = "Onaityte",
                    Address = "Kaunas, KVilniaus gatve 3",
                    CreationTimeUtc = DateTime.UtcNow
                }
            };
        }
        public async Task<UserList> Get(int id)
        {
            return new UserList()
            {
                UserName = "Marijukasss5",
                Name = "Marija",
                Surname = "Leonaviciute",
                Address = "Kaunas, Kauno gatve 3",
                CreationTimeUtc = DateTime.UtcNow
            };

        }
        public async Task<UserList> Create(UserList userList)
        {
            return new UserList()
            {
                UserName = "Marijukasss5",
                Name = "Marija",
                Surname = "Leonaviciute",
                Address = "Kaunas, Kauno gatve 3",
                CreationTimeUtc = DateTime.UtcNow
            };
        }
        public async Task<UserList> Put(UserList userList)
        {
            return new UserList()
            {
                UserName = "Marijukasss5",
                Name = "Marija",
                Surname = "Leonaviciute",
                Address = "Kaunas, Kauno gatve 3",
                CreationTimeUtc = DateTime.UtcNow
            };
        }
        public async Task Delete(UserList userList)
        {
        }
    }
}
