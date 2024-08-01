using PersonalFinanceManagement.Service.DTOs.Budget;
using PersonalFinanceManagement.Service.DTOs.Income;

namespace PersonalFinanceManagement.Service.Interfaces;

public interface IBudgetService
{

    public Task<bool> CreateAsync(BudgetForCreationDto budget);
    public Task<bool> UpdateAsync(BudgetForUpdateDto budget, int id);
    public Task<bool> DeleteAsync(int id);
    public Task<BudgetForResultDto> GetByIdAsync(int id);
    public Task<IEnumerable<BudgetForResultDto>> GetAllAsync();
    public Task<IEnumerable<BudgetForResultDto>> GetByUserIdAsync(int id);


}
