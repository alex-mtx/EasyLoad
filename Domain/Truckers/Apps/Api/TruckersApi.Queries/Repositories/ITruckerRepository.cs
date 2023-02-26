namespace TruckersApi.Queries.Repositories
{
    public interface ITruckerRepository
    {
        Task<IEnumerable<Trucker>> GetAll(TruckersByLocationQuery query, CancellationToken ct);
    }
}
