using PersonalFinanceManagement.Service.DTOs.Budget;
using PersonalFinanceManagement.Service.DTOs.Income;

namespace PersonalFinanceManagement.Service.Interfaces;

public interface IIncomeService
{
    public Task<bool> CreateAsync(IncomeForCreationDto income);
    public Task<bool> UpdateAsync(IncomeForUpdateDto income, int incomeId);
    public Task<bool> DeleteAsync(int id);
    public Task<IncomeForResultDto> GetByIdAsync(int id);
    public Task<IEnumerable<IncomeForResultDto>> GetAllAsync();
    public Task<IEnumerable<IncomeForResultDto>> GetByUserIdAsync(int id);


}
