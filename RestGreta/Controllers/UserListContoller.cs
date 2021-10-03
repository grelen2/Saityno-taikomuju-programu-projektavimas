using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Dtos.UsersList;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;


namespace RestGreta.Controllers
{
    [ApiController]
    [Route("api/userList")]
    public class UserListContoller: ControllerBase
    {
        private readonly IUserListRepository _userListRepository;
        private readonly IMapper _mapper;
        public UserListContoller(IUserListRepository userListRepository, IMapper mapper)
        {
            _userListRepository = userListRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserListDto>> GetAll()
        {
            return (await _userListRepository.GetAll()).Select(o => _mapper.Map<UserListDto>(o));
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<UserListDto>> Get(int id)
        {
            var userList = await _userListRepository.Get(id);
            if (userList == null) return NotFound($"User with id'{id}'not found");
            return _mapper.Map<UserListDto>(userList);


        }
        [HttpPost]
        public async Task<ActionResult<UserListDto>> Post(CreateUserListDto userListDto)
        {
            var userList = _mapper.Map<UserList>(userListDto);
            await _userListRepository.Create(userList);
            return Created($"/api/userList/{userList.Id}", _mapper.Map<UserListDto>(userList));
        }
        [HttpPut(template: "{id}")]
        public async Task<ActionResult<UserListDto>> Put(int id, UpdateUserListDto userListDto)
        {
            var userList = await _userListRepository.Get(id);
            if (userList == null) return NotFound($"User with id'{id}'not found");

            userList.UserName = userListDto.UserName;
            userList.Name = userListDto.Name; 
            userList.Surname = userListDto.Surname;
            userList.Address = userListDto.Address;


            await _userListRepository.Put(userList);
            return _mapper.Map<UserListDto>(userList);
        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<UserListDto>> Delete(int id)
        {
            var userList = await _userListRepository.Get(id);
            if (userList == null) return NotFound($"User with id'{id}'not found");

            await _userListRepository.Delete(userList);
            return NoContent();
        }
    }
}
