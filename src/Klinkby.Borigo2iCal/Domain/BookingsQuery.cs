namespace Klinkby.Borigo2iCal.Domain;

public record BookingsQuery(int FacilityId, DateTimeOffset Date, int Offset = 0);