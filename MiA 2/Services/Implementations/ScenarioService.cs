using Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class ScenarioService : IScenarioService
{
    private readonly IScenarioRepository _repo;

    public ScenarioService(IScenarioRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Scenario>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Scenario?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task<Scenario> CreateAsync(Scenario scenario) => _repo.CreateAsync(scenario);

    public Task<Scenario?> UpdateAsync(int id, Scenario scenario) => _repo.UpdateAsync(id, scenario);

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
}
