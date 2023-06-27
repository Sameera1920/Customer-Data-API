using System;
using AutoMapper;
using TestApp1.Models;
using TestApp1.Models.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserUpdateRequestDTO, User>();
        CreateMap<AddressDTO, Address>();
    }
}

