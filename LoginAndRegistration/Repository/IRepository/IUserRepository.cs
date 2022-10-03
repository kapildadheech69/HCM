﻿using LoginAndRegistration.Dto;
using LoginAndRegistration.Modals;

namespace LoginAndRegistration.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<LocalUser> Register(RegistrationRequestDto registrationRequestDto);    
    }
}
