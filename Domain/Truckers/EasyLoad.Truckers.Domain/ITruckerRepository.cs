
namespace EasyLoad.Truckers.Domain
{
    public interface ITruckerRepository
    {
        Task<Trucker?> GetAsync(string id, CancellationToken ct = default);
        Task UpdateAsync(string id, Trucker item, CancellationToken ct);

    }
}
