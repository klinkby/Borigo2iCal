namespace Klinkby.Borigo2iCal;

public record ApiClientOptions
{
    public string RememberUserToken { get; init; } = "";
    public string Subdomain { get; init; } = "";
    public int FacilityId { get; init; } = 0;
}