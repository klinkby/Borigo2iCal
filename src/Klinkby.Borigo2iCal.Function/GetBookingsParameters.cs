using System;

namespace Klinkby.Borigo2iCal.Function;

public record GetBookingsParameters
{
    public DateTime Date { get; init; } = DateTime.Now.AddDays(-4).Date;
}