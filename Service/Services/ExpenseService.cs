using PersonalFinanceManagement.Data.IRepositories;
using PersonalFinanceManagement.Domain.Entities;
using PersonalFinanceManagement.Service.DTOs.Expense;
using PersonalFinanceManagement.Service.DTOs.Income;
using PersonalFinanceManagement.Service.Exceptions;
using PersonalFinanceManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceManagement.Service.Services;

public class ExpenseService : IExpenseService
{

    IUserService userService = new UserService();
    IRepository<Expense> expenseRepository = new Repository<Expense>();
    public async Task<bool> CreateAsync(ExpenseForCreationDto expense)
    {
        if (expense.Amount < 0)
            throw new PersonalFinanceException(0, "Amount can not be less than 0 ");


        var mapped = new Expense()
        {
            UserId = expense.UserId,
            Amount = expense.Amount,
            Category = expense.Category,
            Description  = expense.Description,
        };

        bool isCreated = await expenseRepository.InsertAsync(mapped);
        return isCreated;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var income = expenseRepository.SelectAllAsync().Result.Where(x => x.Id == id);
        if (income == null)
            throw new PersonalFinanceException(404, "Budget not found");
        var isDeleted = await expenseRepository.DeleteByIdAsync(id);
        return isDeleted;
    }

    public async Task<IEnumerable<ExpenseForResultDto>> GetAllAsync()
    {
        var expenses = await expenseRepository.SelectAllAsync();
        List<ExpenseForResultDto> expenseForResult = new List<ExpenseForResultDto>();

        foreach (var expense in expenses)
        {
            //var mappedUser = await userService.GetByIdAsync(budget.UserId);

            var mappedBudget = new ExpenseForResultDto()
            {
                Id = expense.Id,
                UserId = userService.GetByIdAsync(expense.UserId).Result,
                Amount = expense.Amount,
                Category = expense.Category,
                Description = expense.Description,
                CreatedAt = expense.CreatedAt


            };

            expenseForResult.Add(mappedBudget);
        }
        return expenseForResult;



    }

    public async Task<ExpenseForResultDto> GetByIdAsync(int id)
    {

        ExpenseForResultDto mappedBudget = GetAllAsync().Result.Where(e => e.Id == id).FirstOrDefault();
        
        if (mappedBudget == null)
            throw new PersonalFinanceException(404, "Expense not found");

        return mappedBudget;
    }

    public async Task<IEnumerable<ExpenseForResultDto>> GetByUserIdAsync(int id)
    {

        var expense = GetAllAsync().Result.Where(e => e.UserId.Id == id);
        return expense;

    }

    public async Task<bool> UpdateAsync(ExpenseForUpdateDto expense, int expenseId)
    {

        var mappedIncome = new Expense()
        {
            Id = expenseId,
            UserId = expense.UserId,
            Amount = expense.Amount,
            Category = expense.Category,
            Description = expense.Description,
            CreatedAt = expense.CreatedAt
        };

        var isUpdated = await expenseRepository.UpdateAsync(mappedIncome);
        return isUpdated;

    }

}
