using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using VisualMerchandiser.Models.Domain;
using VisualMerchandiser.Models.Domain.Repositories;

namespace VisualMerchandiser.Models.Application
{

    public interface IAccountService
    {
        LoginResponse Login(LoginRequest request);
    }

    public class LoginResponseProfile : Profile
    {
        public LoginResponseProfile()
        {
            CreateMap<User, LoginResponse>()
                .ForMember(destination => destination.UserId, options => options.MapFrom(user => user.Id));
        }
    }

    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<User> _userRepository;

        public AccountService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public LoginResponse Login(LoginRequest request)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<LoginResponseProfile>();
            });
            var user = _userRepository
                .All()
                .SingleOrDefault(u => u.UserName == request.UserName);
            if (user == null)
            {
                return LoginResponse.CreateFailure("You have entered an invalid username or password");
            }
            var response = LoginResponse.CreateSuccess();
            Mapper.Map(user, response);
            return response;
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ParentUserId { get; set; }
        public bool Success { get; set; }
        public string FailureText { get; set; }

        public static LoginResponse CreateFailure(string failureText)
        {
            return new LoginResponse()
            {
                Success = false,
                FailureText = failureText
            };
        }

        public static LoginResponse CreateSuccess()
        {
            return new LoginResponse()
            {
                Success = true
            };
        }
    }
}
