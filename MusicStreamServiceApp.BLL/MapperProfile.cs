using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.BLL.DTOs;

namespace MusicStreamServiceApp.BLL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Music, MusicCUDTO>()
                .ForMember(e => e.Album, opt => opt.Ignore());

            CreateMap<MusicCUDTO, Music>()
                .ForMember(e => e.Album, opt => opt.Ignore());

            CreateMap<Music, MusicViewDTO>()
                .ReverseMap();

            CreateMap<Genre, GenreDTO>()
                .ReverseMap();

            CreateMap<Album, AlbumDTO>()
                .ReverseMap();

            CreateMap<UserPlaylist, PlaylistDTO>()
                .ReverseMap();

            CreateMap<UserPlaylist, PlaylistCUDTO>()
                .ForMember(e => e.Name, opt => opt.MapFrom(e => e.PlaylistName))
                .ReverseMap();

            CreateMap<MusicPlaylist, MusicPlaylistDTO>()
                .ReverseMap();

            //Identity
            CreateMap<UserDTO, User>()
                .ForMember(e => e.Id, opt => opt.Ignore());

            CreateMap<User, UserDTO>();

            CreateMap<User, UserLoginDTO>()
                .ReverseMap();

            CreateMap<User, UserUpdateDTO>()
                .ReverseMap();
        }
    }
}
