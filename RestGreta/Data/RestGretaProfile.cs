using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RestGreta.Data.Dtos.Products;
using RestGreta.Data.Dtos.Comments;
using RestGreta.Data.Dtos.UsersList;
using RestGreta.Data.Dtos.Recipies;
using RestGreta.Data.Entities;

namespace RestGreta.Data
{
    public class RestGretaProfile: Profile
    {
        public RestGretaProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UserList, UserListDto>();
            CreateMap<CreateUserListDto, UserList>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<CreateRecipeDto, Recipe>();
        }
    }
}
