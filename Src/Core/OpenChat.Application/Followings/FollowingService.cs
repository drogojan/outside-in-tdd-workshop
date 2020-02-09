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
    }
}