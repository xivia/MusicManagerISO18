using MusicManager.Server.Core.Model;
using System.Collections.Generic;

namespace MusicManager.Server.Core.DataTransferObjects.Mapper
{
    public class UserResponseDtoMapper
    {

        public static List<UserResponseDto> FromDb(List<User> dbUsers)
        {
            List<UserResponseDto> userResponseDtos = new List<UserResponseDto>();

            dbUsers.ForEach(user =>
            {
                userResponseDtos.Add(new UserResponseDto
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    EmailAddress = user.EmailAddress,
                    Banned = user.Banned
                });
            });

            return userResponseDtos;
        }

        public static UserResponseDto FromDb(User dbUser)
        {
            if (dbUser is null)
                return null;

            return new UserResponseDto
            {
                UserId = dbUser.UserId,
                Name = dbUser.Name,
                EmailAddress = dbUser.EmailAddress,
                Banned = dbUser.Banned
            };
        }

    }
}
