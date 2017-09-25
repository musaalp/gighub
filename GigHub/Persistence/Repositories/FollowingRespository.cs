using GigHub.Core.Models;
using GigHub.Persistence;
using System.Linq;

namespace GigHub.Core.Repositories
{
    public class FollowingRespository : IFollowingRespository
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

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }
    }
}