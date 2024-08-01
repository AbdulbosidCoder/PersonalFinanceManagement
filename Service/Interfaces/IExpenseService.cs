using PersonalFinanceManagement.Service.DTOs.Expense;
using PersonalFinanceManagement.Service.DTOs.Income;

namespace PersonalFinanceManagement.Service.Interfaces;

public interface IExpenseService
{
    public Task<bool> CreateAsync(ExpenseForCreationDto expense);
    public Task<bool> UpdateAsync(ExpenseForUpdateDto expense , int expenseId);
    public Task<bool> DeleteAsync(int id);
    public Task<ExpenseForResultDto> GetByIdAsync(int id);
    public Task<IEnumerable<ExpenseForResultDto>> GetAllAsync();
    public Task<IEnumerable<ExpenseForResultDto>> GetByUserIdAsync(int username);

}
