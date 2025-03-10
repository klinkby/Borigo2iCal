using System.Globalization;
using Klinkby.VCard;

namespace Klinkby.Borigo2iCal;

internal static class BookingsResponseExtensions
{
    public static VCalendar ToVCalendar(this BookingsResponse bookingsResponse, UsersResponse usersResponse)
    {
        ArgumentNullException.ThrowIfNull(bookingsResponse);
        ArgumentNullException.ThrowIfNull(usersResponse);
        var defaultUser = new User { Id = -1, FirstName = "(n/a)" };
        var vCalendar = new VCalendar
        {
            Events = from b in bookingsResponse.Orders
                where b.Status == "ACTIVE"
                let u = usersResponse.Users.FirstOrDefault(u => u.Id == b.UserId) ?? defaultUser
                select new VEvent
                {
                    DtStart = b.Start.UtcDateTime,
                    DtEnd = b.End.UtcDateTime,
                    DtStamp = b.Created.UtcDateTime,
                    UId = b.Id.ToString(CultureInfo.InvariantCulture),
                    Summary = $"{bookingsResponse.Name}",
                    Description = b.UserComment,
                    Organizer = $@"CN=\""{u.FirstName} {u.LastName}\"":MAILTO:{u.Email}",
                    Location = bookingsResponse.Name
                }
        };
        return vCalendar;
    }
}
