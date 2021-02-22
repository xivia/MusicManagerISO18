using MusicManager.Server.Core.DataTransferObjects.SongDtos;
using MusicManager.Server.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects.Mapper
{
    public class SongResponseDtoMapper
    {

        public static SongResponseDto DbToDto(Song song)
        {
            Byte[] coverFileBytes = File.ReadAllBytes(song.CoverFilePath);
            string coverFileBase64 = Convert.ToBase64String(coverFileBytes);

            var tFile = TagLib.File.Create(song.FilePath, TagLib.ReadStyle.Average);
            TimeSpan duration = tFile.Properties.Duration;

            return new SongResponseDto
            {
                SongId = song.SongId,
                Genre = song.SongGenre,
                Name = song.Name,
                Artist = UserResponseDtoMapper.FromDb(song.Artist),
                PublishOn = song.PublishOn,
                CoverFileBase64 = coverFileBase64,
                Duration = duration.TotalMinutes.ToString("00:00")
            };
        }

        public static List<SongResponseDto> DbToDto(List<Song> songs)
        {
            return songs.Select(song => DbToDto(song)).ToList();
        }

    }
}
