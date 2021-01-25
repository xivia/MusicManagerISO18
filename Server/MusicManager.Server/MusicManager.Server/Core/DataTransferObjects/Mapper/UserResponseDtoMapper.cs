using MusicManager.Server.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace MusicManager.Server.Core.DataTransferObjects.Mapper
{
    public class UserResponseDtoMapper
    {

        public static List<UserResponseDto> FromDb(List<User> dbUsers)
        {
            return dbUsers.Select(FromDb).ToList();
        }

        public static UserResponseDto FromDb(User dbUser)
        {
            if (dbUser is null)
                return null;

            return new UserResponseDto
            {
                UserId = dbUser.UserId,
                Name = dbUser.Name,
                Banned = dbUser.Banned
            };
        }

    }
}
