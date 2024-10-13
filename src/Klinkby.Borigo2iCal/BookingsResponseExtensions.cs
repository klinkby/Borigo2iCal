using System.Globalization;
using Klinkby.VCard;

namespace Klinkby.Borigo2iCal;

internal static class BookingsResponseExtensions
{
    public static VCalendar ToVCalendar(this BookingsResponse bookingsResponse)
    {
        ArgumentNullException.ThrowIfNull(bookingsResponse);

        var vCalendar = new VCalendar
        {
            Events = from b in bookingsResponse.Orders
                where b.Status == "ACTIVE"
                select new VEvent(b.Start.UtcDateTime, b.End.UtcDateTime, b.Created.UtcDateTime)
                {
                    UId = b.Id.ToString(CultureInfo.InvariantCulture),
                    Summary = $"{bookingsResponse.Name} ({b.UserId})",
                    Description = string.Empty,
                    Organizer = $"{b.UserId}",
                    Location = bookingsResponse.Name
                }
        };
        return vCalendar;
    }
}
