using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRespository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRespository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string followeeId, string userId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == userId);
        }
    }
}