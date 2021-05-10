using MusicManager.Server.Core.DataTransferObjects.PlaylistDtos;
using MusicManager.Server.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects.Mapper
{
    public class PlaylistMapper
    {
        public static Playlist DtoToDb(PlaylistDtos.PlaylistDto playlistDto)
        {
            return new Playlist
            {
                Name = playlistDto.Name,
                PlaylistId = playlistDto.PlaylistId,
                Songs = playlistDto.Songs,
                User = playlistDto.User,
                UserId = playlistDto.User.UserId
            };
        }

        public static PlaylistDto DbToDto(Playlist playlist)
        {
            return new PlaylistDto
            {
                Name = playlist.Name,
                PlaylistId = playlist.PlaylistId,
                Songs = playlist.Songs,
                User = playlist.User
            };
        }
    }
}
