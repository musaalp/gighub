using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRespository
    {
        Following GetFollowing(string followeeId, string userId);
    }
}