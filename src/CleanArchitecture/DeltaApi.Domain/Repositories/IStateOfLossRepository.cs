using DeltaApi.Domain.Claims;

namespace DeltaApi.Domain.Repositories;

public interface IStateOfLossRepository
{
    Task<StateOfLoss?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<StateOfLoss?> GetByStateCodeAsync(string stateCode, CancellationToken cancellationToken = default);
    Task<IEnumerable<StateOfLoss>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StateOfLoss> AddAsync(StateOfLoss stateOfLoss, CancellationToken cancellationToken = default);
    Task UpdateAsync(StateOfLoss stateOfLoss, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
