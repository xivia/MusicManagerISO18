using MusicManager.Server.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects.Mapper
{
    public class UserDtoMapper
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
                    Banned = user.Banned
                });
            });

            return userResponseDtos;
        }

        public static UserResponseDto FromDb(User dbUser)
        {
            return new UserResponseDto
            {
                UserId = dbUser.UserId,
                Name = dbUser.Name,
                Banned = dbUser.Banned
            };
        }

    }
}
