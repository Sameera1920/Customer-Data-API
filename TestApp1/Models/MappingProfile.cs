using System;
using AutoMapper;
using TestApp1.Models;
using TestApp1.Models.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserUpdateDTO, User>();
        CreateMap<AddressUpdateDTO, Address>();
        CreateMap<AddressDTO, Address>();


    }
}

