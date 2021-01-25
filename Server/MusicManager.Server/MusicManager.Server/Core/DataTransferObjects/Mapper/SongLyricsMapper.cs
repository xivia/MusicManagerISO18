using MusicManager.Server.Core.DataTransferObjects.SongLyricsDtos;
using MusicManager.Server.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects.Mapper
{
    public class SongLyricsMapper
    {
        public static SongLyricsResponseDto DbToDto(SongLyrics songLyrics)
        {
            return new SongLyricsResponseDto
            {
                SongLyricsId = songLyrics.SongLyricsId,
                SongLyrics = songLyrics.Lyrics,
                Song = SongResponseDtoMapper.DbToDto(songLyrics.Song)
            };
        }

        public static List<SongLyricsResponseDto> DbToDto(List<SongLyrics> songLyrics)
        {
            return songLyrics.Select(DbToDto).ToList();
        }
    }
}
