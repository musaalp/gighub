﻿using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                    .Include(p => p.Attendances.Select(k => k.Attendee))
                    .SingleOrDefault(p => p.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                    .Where(a => a.AttendeeId == userId)
                    .Select(a => a.Gig)
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigsByUserId(string userId)
        {
            return _context.Gigs
                    .Where(g =>
                    g.ArtistId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                    .Include(g => g.Genre)
                    .ToList();
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .SingleOrDefault(p => p.Id == gigId);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);            
        }
    }
}