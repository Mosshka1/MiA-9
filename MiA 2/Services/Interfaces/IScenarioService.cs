using Models;

namespace Services.Interfaces;

public interface IScenarioService
{
    Task<List<Scenario>> GetAllAsync();
    Task<Scenario?> GetByIdAsync(int id);
    Task<Scenario> CreateAsync(Scenario scenario);
    Task<Scenario?> UpdateAsync(int id, Scenario scenario);
    Task<bool> DeleteAsync(int id);
}
