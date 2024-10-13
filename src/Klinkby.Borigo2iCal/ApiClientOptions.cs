using System.ComponentModel.DataAnnotations;

namespace Klinkby.Borigo2iCal;

public sealed record ApiClientOptions
{
    [Required] public required string RememberUserToken { get; init; }

    [Required] public required string Subdomain { get; init; }
}
