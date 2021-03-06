﻿using AutoMapper;
using Models.MessageModels;
using Services.Dto;
using Services.Dto.MessageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Models.UserModels;

namespace Services.Shared
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap()
                .ForMember(x => x.Id, option => option.Ignore());

            CreateMap<User, RegisterDto>()
                .ReverseMap()
                .ForMember(x => x.Id, option => option.Ignore());

            CreateMap<Message, SendMsgDTO>()
                .ForMember(x => x.Cc, opt => opt.MapFrom(x => x.MessageRecievers.Where(x => x.IsCc).Select(s => s.UserId)))
                .ForMember(x => x.To, opt => opt.MapFrom(x => x.MessageRecievers.Where(x => !x.IsCc).Select(s => s.UserId)))
                .ReverseMap()
                .ForMember(x => x.MessageRecievers, opt => opt.Ignore());

            CreateMap<Message, MsgBoxDTO>()
                .ReverseMap()
                .ForMember(x => x.Id, option => option.Ignore());

            CreateMap<MessageSender, MsgBoxDTO>()
                .ForMember
                (x => x.To, option => option
                   .MapFrom(src => src.MessageRecievers
                   .Where(p => p.IsCc == false).Select(x => x.UserId)))
                   .ForMember
                (x => x.Cc, option => option
                   .MapFrom(src => src.MessageRecievers
                   .Where(p => p.IsCc == true).Select(x => x.UserId)))
                .ForMember(x => x.ImportanceLevel, option => option
                .MapFrom(src => src.Message.ImportanceLevel))
                .ForMember(x => x.DueDate, option => option
                .MapFrom(src => src.Message.DueDate));

            CreateMap<MessageReciever, MsgBoxDTO>();

            CreateMap<MessageSender, ReplyMessageDTO>();
        }
    }
}