using MusicManager.Server.Core.DataTransferObjects.SongDtos;
using MusicManager.Server.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects.Mapper
{
    public class SongResponseDtoMapper
    {

        public static SongResponseDto DbToDto(Song song)
        {
            return new SongResponseDto
            {
                Genre = song.SongGenre,
                Name = song.Name,
                Artist = UserResponseDtoMapper.FromDb(song.Artist),
                PublishOn = song.PublishOn
            };
        }

        public static List<SongResponseDto> DbToDto(List<Song> songs)
        {
            return songs.Select(song => DbToDto(song)).ToList();
        }

    }
}
