using System.Diagnostics.CodeAnalysis;

namespace Klinkby.Borigo2iCal.Func;

[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
internal sealed record GetBookingsParameters
{
    public int Facility { get; init; }
}
