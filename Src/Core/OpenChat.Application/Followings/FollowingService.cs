using System.Linq;
using System;
using System.Collections.Generic;
using OpenChat.Application.Users;
using OpenChat.Domain.Entities;

namespace OpenChat.Application.Followings
{
    public class FollowingService : IFollowingService
    {
        private IFollowingRepository followingRepository;

        public FollowingService(IFollowingRepository followingRepository)
        {
            this.followingRepository = followingRepository;
        }

        public void CreateFollowing(FollowingInputModel following)
        {
            Following newFollowing = new Following
            {
                FollowerId = following.FollowerId, 
                FolloweeId = following.FolloweeId
            };
            
            if (followingRepository.IsFollowingRegistered(newFollowing))
            {
                throw new FollowingAlreadyExistsException();
            }

            followingRepository.Add(newFollowing);
        }

        public IEnumerable<UserApiModel> UsersFollowedBy(Guid userId)
        {
            var followees = followingRepository.UsersFollowedBy(userId);
            return followees.Select(u =>
                new UserApiModel {
                    Id = u.Id,
                    Username = u.Username,
                    About = u.About
                }
            );
        }
    }
}