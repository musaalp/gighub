﻿using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRespository Followings { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IApplicationDbRepository Users { get; }
        IUserNotificationRepository UserNotifications { get; }
        INotificationRepository Notifications { get; }
        void Complete();
    }
}