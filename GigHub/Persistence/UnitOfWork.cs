using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public GigRepository Gigs { get; private set; }
        public AttendanceRepository Attendances { get; private set; }
        public FollowingRespository Followings { get; private set; }
        public GenreRepository Genres { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Followings = new FollowingRespository(_context);
            Genres = new GenreRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}