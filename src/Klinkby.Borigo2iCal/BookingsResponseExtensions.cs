using Klinkby.Borigo2iCal.Domain;
using Klinkby.VCard;

namespace Klinkby.Borigo2iCal;

public static class BookingsResponseExtensions
{
    public static VCalendar ToVCalendar(this BookingsResponse bookingsResponse)
    {
        var vCalendar = new VCalendar
        {
            Events = from b in bookingsResponse.Bookings
                where b.Status == "approved"
                select new VEvent(b.StartsAt.UtcDateTime, b.EndsAt.UtcDateTime, b.CreatedAt.UtcDateTime)
                {
                    UId = b.Id.ToString(),
                    Summary = $"{b.Description} ({b.Responsible.Name})",
                    Description = b.Description,
                    Organizer = b.Responsible.Name,
                    Location = bookingsResponse.Facility.Title
                }
        };
        return vCalendar;
    }
}