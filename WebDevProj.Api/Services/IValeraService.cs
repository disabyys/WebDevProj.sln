using WebDevProj.Api.Models;

namespace WebDevProj.Api.Services;

public interface IValeraService
{
    Task<IEnumerable<Valera>> GetAllAsync();
    Task<Valera?> GetByIdAsync(int id);
    Task<Valera> CreateAsync(Valera valera);
    Task<Valera?> UpdateAsync(int id, Valera valera);
    Task<bool> DeleteAsync(int id);
}
