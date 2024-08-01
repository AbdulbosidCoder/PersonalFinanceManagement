using PersonalFinanceManagement.Data.IRepositories;
using PersonalFinanceManagement.Domain.Entities;
using PersonalFinanceManagement.Service.DTOs.Budget;
using PersonalFinanceManagement.Service.DTOs.Income;
using PersonalFinanceManagement.Service.Exceptions;
using PersonalFinanceManagement.Service.Interfaces;

namespace PersonalFinanceManagement.Service.Services;

public class IncomeService : IIncomeService
{
    IUserService userService = new UserService();
    IRepository<Income> incomeRepository = new Repository<Income>();
    public async Task<bool> CreateAsync(IncomeForCreationDto Income)
    {
        if (Income.Amount < 0)
            throw new PersonalFinanceException(0, "Amount can not be less than 0 ");


        var mapped = new Income()
        {
            UserId = Income.UserId,
            Amount = Income.Amount,
            Source = Income.Source
        };

        bool isCreated = await incomeRepository.InsertAsync(mapped);
        return isCreated;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var income = incomeRepository.SelectAllAsync().Result.Where(x => x.Id == id);
        if (income == null)
            throw new PersonalFinanceException(404, "Budget not found");
        var isDeleted = await incomeRepository.DeleteByIdAsync(id);
        return isDeleted;
    }

    public async Task<IEnumerable<IncomeForResultDto>> GetAllAsync()
    {
        var incomes = await incomeRepository.SelectAllAsync();
        List<IncomeForResultDto> incomeForResult = new List<IncomeForResultDto>();

        foreach (var income in incomes)
        {
            //var mappedUser = await userService.GetByIdAsync(budget.UserId);

            var mappedBudget = new IncomeForResultDto()
            {
                Id = income.Id,
                User = userService.GetByIdAsync(income.UserId).Result,
                Amount = income.Amount,
                CreatedAt = income.CreatedAt


            };

            incomeForResult.Add(mappedBudget);
        }
        return incomeForResult;



    }

    public async Task<IncomeForResultDto> GetByIdAsync(int id)
    {

        IncomeForResultDto mappedIncome = GetAllAsync().Result.Where(e => e.Id == id).FirstOrDefault();
        if (mappedIncome == null)
            throw new PersonalFinanceException(404, "Income not found");


        return mappedIncome;
    }

    public async Task<IEnumerable<IncomeForResultDto>> GetByUserIdAsync(int id)
    {

        var income = GetAllAsync().Result.Where(e => e.User.Id == id);
        if (income.Any())
            return income;
        throw new PersonalFinanceException(404,  "Income not found ");
    }

    public async Task<bool> UpdateAsync(IncomeForUpdateDto income, int incomeId)
    {

        var mappedIncome = new Income()
        {
            Id = incomeId,
            UserId = income.UserId,
            Amount = income.Amount,
            Source = income.Source,
            CreatedAt = income.CreatedAt
        };

        var isUpdated = await incomeRepository.UpdateAsync(mappedIncome);
        return isUpdated;

    }
}
