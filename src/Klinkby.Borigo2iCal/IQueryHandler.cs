namespace Klinkby.Borigo2iCal;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : notnull
    where TResponse : notnull
{
    Task<TResponse> ExecuteQueryAsync(TQuery request, CancellationToken cancellationToken);
}