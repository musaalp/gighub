using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IFollowingRespository
    {
        Following GetFollowing(string followeeId, string userId);
    }
}