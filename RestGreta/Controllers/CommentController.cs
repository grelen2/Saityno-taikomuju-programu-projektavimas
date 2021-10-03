using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Dtos.Comments;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetAll()
        {
            return (await _commentRepository.GetAll()).Select(o => _mapper.Map<CommentDto>(o));
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<CommentDto>> Get(int id)
        {
            var comment = await _commentRepository.Get(id);
            if (comment == null) return NotFound($"Comment with id'{id}'not found");
            return _mapper.Map<CommentDto>(comment);


        }
       [HttpPost]
        public async Task<ActionResult<CommentDto>> Post(CreateCommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            await _commentRepository.Create(comment);
            return Created($"/api/comments/{comment.Id}", _mapper.Map<CommentDto>(comment));
        }
        [HttpPut(template: "{id}")]
        public async Task<ActionResult<CommentDto>> Put(int id, UpdateCommentDto commentDto)
        {
            var comment = await _commentRepository.Get(id);
            if (comment == null) return NotFound($"Comment with id'{id}'not found");

            comment.UserName = commentDto.UserName;
            comment.CommentText = commentDto.CommentText;

            await _commentRepository.Put(comment);
            return _mapper.Map<CommentDto>(comment);
        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<CommentDto>> Delete(int id)
        {
            var comment = await _commentRepository.Get(id);
            if (comment == null) return NotFound($"Comment with id'{id}'not found");

            await _commentRepository.Delete(comment);
            return NoContent();
        }
    }
}
