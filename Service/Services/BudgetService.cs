using PersonalFinanceManagement.Data.IRepositories;
using PersonalFinanceManagement.Domain.Entities;
using PersonalFinanceManagement.Service.DTOs.Budget;
using PersonalFinanceManagement.Service.Exceptions;
using PersonalFinanceManagement.Service.Interfaces;
using System.Security.Principal;

namespace PersonalFinanceManagement.Service.Services;

public class BudgetService : IBudgetService
{
    IUserService userService = new UserService();
    IRepository<Budget> budgetRepository = new Repository<Budget>();
    public async Task<bool> CreateAsync(BudgetForCreationDto budget)
    {
        if (budget.Amount < 0)
            throw new PersonalFinanceException(0, "Amount can not be less than 0 ");


        var mapped = new Budget()
        {
            UserId = budget.UserId,
            Category = budget.Category,
            Amount = budget.Amount,
            StartDate = budget.StartDate,
            EndDate = budget.EndDate
        };

        bool isCreated = await budgetRepository.InsertAsync(mapped);
        return isCreated;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var budget = budgetRepository.SelectAllAsync().Result.Where(x => x.Id == id);
        if (budget == null)
            throw new PersonalFinanceException(404, "Budget not found");
        var isDeleted = await budgetRepository.DeleteByIdAsync(id);
        return isDeleted;
    }

    public async Task<IEnumerable<BudgetForResultDto>> GetAllAsync()
    {
        var budgets = await budgetRepository.SelectAllAsync();
        List<BudgetForResultDto> budgetsForResult = new List<BudgetForResultDto>();
        
        foreach (var  budget in budgets)
        {
            //var mappedUser = await userService.GetByIdAsync(budget.UserId);

            var mappedBudget = new BudgetForResultDto()
            {
                Id = budget.Id,
                User = userService.GetByIdAsync(budget.UserId).Result,
                Category = budget.Category,
                Amount = budget.Amount,
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
                CreatedAt = budget.CreatedAt,


            };

            budgetsForResult.Add(mappedBudget);
        }
        return budgetsForResult;



    }

    public async Task<BudgetForResultDto> GetByIdAsync(int id)
    { 

        BudgetForResultDto mappedBudget = GetAllAsync().Result.Where(e=>e.Id == id).FirstOrDefault();
        
        return mappedBudget;
    }

    public async Task<IEnumerable<BudgetForResultDto>> GetByUserIdAsync(int id)
    {

        var budgets = GetAllAsync().Result.Where(e=> e.User.Id == id);
        return budgets;
        
    }

    public async Task<bool> UpdateAsync(BudgetForUpdateDto budget, int id)
    {

        var mappedBudget = new Budget()
        {
            Id =  id,
            UserId = budget.UserId,
            Category = budget.Category,
            Amount = budget.Amount,
            StartDate = budget.StartDate,
            EndDate = budget.EndDate,

        };

        var isUpdated = await budgetRepository.UpdateAsync(mappedBudget);
        return isUpdated;

    }
}
