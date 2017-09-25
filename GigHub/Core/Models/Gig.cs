using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }
        
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }
        
        public string Venue { get; set; }

        public Genre Genre { get; set; }
        
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        internal void Cancel()
        {
            this.IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        internal void Modify(DateTime datetime, string venue, byte genre)
        {
            var notification = Notification.GigUpdated(this, datetime, venue);            

            Venue = venue;
            DateTime = datetime;
            GenreId = genre;

            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}