using PersonalFinanceManagement.Service.DTOs.Budget;
using PersonalFinanceManagement.Service.DTOs.Expense;
using PersonalFinanceManagement.Service.DTOs.Income;
using PersonalFinanceManagement.Service.DTOs.User;
using PersonalFinanceManagement.Service.Exceptions;
using PersonalFinanceManagement.Service.Interfaces;
using PersonalFinanceManagement.Service.Services;

namespace PersonalFinanceManagement.Pressentations;

public class Pressentation
{

    IUserService userService = new UserService();
    IBudgetService budgetService = new BudgetService();
    IIncomeService incomeService = new IncomeService();
    IExpenseService expenseService = new ExpenseService();

    public async Task ShowForUserAsync(int userID)
    {


        try
        {
            while (true)
            {
                
                await Console.Out.WriteLineAsync("Choose Option");
                await Console.Out.WriteLineAsync("1 = > Add Income");
                await Console.Out.WriteLineAsync("2 = > Add Expense");
                await Console.Out.WriteLineAsync("3 = > Set Budget");
                await Console.Out.WriteLineAsync("4 => My history");
                await Console.Out.WriteLineAsync("5 => My Profile");
                await Console.Out.WriteLineAsync("10 = > {exit}");

                var result = int.Parse(Console.ReadLine());
                switch (result)
                {
                    case 1:
                        {
                            var income = new IncomeForCreationDto()
                            {
                                UserId = userID,
                            };
                            await Console.Out.WriteLineAsync("Enter Income Source");
                            income.Source = Console.ReadLine();
                            await Console.Out.WriteLineAsync("Enter Income Amount");
                            income.Amount = double.Parse(Console.ReadLine());
                            await incomeService.CreateAsync(income);
                            await Console.Out.WriteLineAsync("Successfuly done");

                            break;

                        }
                    case 2:
                        {
                            var expense = new ExpenseForCreationDto()
                            {
                                UserId = userID,
                            };
                            await Console.Out.WriteLineAsync("Enter Income Source");
                            expense.Category = Console.ReadLine();
                            await Console.Out.WriteLineAsync("Enter Income Amount");
                            expense.Amount = double.Parse(Console.ReadLine());
                            await Console.Out.WriteLineAsync("Description");
                            expense.Description = Console.ReadLine();
                            await expenseService.CreateAsync(expense);

                            await Console.Out.WriteLineAsync("Successfuly done");
                            break;
                        }
                    case 3:
                        {
                            var budget = new BudgetForCreationDto()
                            {
                                UserId = userID,
                            };
                            await Console.Out.WriteLineAsync("Enter Income Source");
                            budget.Category = Console.ReadLine();
                            await Console.Out.WriteLineAsync("Enter Income Amount");
                            budget.Amount = double.Parse(Console.ReadLine());
                            await Console.Out.WriteLineAsync("StartDate (yyyy-mm-dd) :");
                            budget.StartDate = DateOnly.Parse(Console.ReadLine());
                            await Console.Out.WriteLineAsync("endDate (yyyy-mm-dd) :");
                            budget.StartDate = DateOnly.Parse(Console.ReadLine());
                            await budgetService.CreateAsync(budget);

                            await Console.Out.WriteLineAsync("Successfuly done");
                            break;
                        }
                    case 4:
                        {
                            await MyHistoryAsync(userID);

                            break;
                        }
                    case 5:
                        {
                            await ChangeProfileAsync(userID);
                            break;
                        }
                    case 10: return;


                }


            }

        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }




    public async Task MyHistoryAsync(int userId)
    {
        try
        {
            while (true)
            {
                await Console.Out.WriteLineAsync("Choose Option");
                await Console.Out.WriteLineAsync("1 => View All Income");
                await Console.Out.WriteLineAsync("2 => View All Expenses");
                await Console.Out.WriteLineAsync("3 => View Budget");
                await Console.Out.WriteLineAsync("4 => Get Total Expenditure and Total Incomes");
                 await Console.Out.WriteLineAsync("10 => {exit}");
                int result = int.Parse(Console.ReadLine());
                switch (result)
                {
                    case 1:
                        {
                            var incomes = await incomeService.GetByUserIdAsync(userId);
                            foreach(var income in incomes)
                            {
                                await Console.Out.WriteLineAsync($"Id {income.Amount} , Amount {income.Amount} , Sourse {income.Source}, CreatedAt {income.CreatedAt}");
                            } 
                            break;

                        }
                    case 2:
                        {
                            var expenses = await expenseService.GetByUserIdAsync(userId);
                            foreach (var expense in expenses)
                            {
                                await Console.Out.WriteLineAsync($"Id {expense.Id} , Amount {expense.Amount} , Sourse {expense.Category},Description {expense.Description}, CreatedAt {expense.CreatedAt}");
                            }
                            break;
                        }
                    case 3:
                        {
                            var budgets = await budgetService.GetByUserIdAsync(userId);
                            foreach (var budget in budgets)
                            {
                                await Console.Out.WriteLineAsync($"Id {budget.Id} , Amount {budget.Amount} , Sourse {budget.Category},StartDate {budget.StartDate},EndDate {budget.EndDate} CreatedAt {budget.CreatedAt}");
                            }
                            break;
                        }
                    case 4:
                        {
                            var expenses = await expenseService.GetByUserIdAsync(userId);
                            var incomes = await incomeService.GetByUserIdAsync(userId);
                            await Console.Out.WriteLineAsync($"Total Expenditure : {expenses.Sum(e => e.Amount)}\nIncome : {incomes.Sum(e=>e.Amount)}");
                            break;
                        }
                    case 10:
                        {
                            return;
                        }
                }

            }

        } catch (PersonalFinanceException ex)
        {
            await Console.Out.WriteLineAsync($"Status Code {ex.StatusCode}");
            await Console.Out.WriteLineAsync(ex.Message);
        } catch (Exception ex) 
        { 
            Console.WriteLine($"{ex.Message}");
        }


    }

    public async Task ChangeProfileAsync(int userId)
    {
        try
        {
            while (true)
            {
                var user = await userService.GetByIdAsync(userId);
                await Console.Out.WriteLineAsync($"1 => FirstName {user.FirstName}");
                await Console.Out.WriteLineAsync($"2 => LastName {user.LastName}");
                await Console.Out.WriteLineAsync($"3 => UserName {user.Username}");
                await Console.Out.WriteLineAsync($"4 => Email {user.Email}");
                await Console.Out.WriteLineAsync($"5 => DateOfBirth {user.DateOfBirth}");
                await Console.Out.WriteLineAsync("10 => (exit)");
                int result = int.Parse(Console.ReadLine());
                switch (result)
                {

                    case 1:
                        {
                            await Console.Out.WriteLineAsync("Enter your New FirstName ");
                            user.FirstName = Console.ReadLine();

                            var userForUpdate = new UserForUpdateDto()
                            {

                                
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                DateOfBirth = user.DateOfBirth,
                                Email   = user.Email,
                                Username = user.Username

                            };
                            await userService.UpdateAsync(userId ,userForUpdate);
                            await Console.Out.WriteLineAsync("Succsefuly created");
                            break;
                        }
                    case 2:
                        {
                            await Console.Out.WriteLineAsync("Enter your New LastName ");
                            user.LastName = Console.ReadLine();

                            var userForUpdate = new UserForUpdateDto()
                            {


                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                DateOfBirth = user.DateOfBirth,
                                Email = user.Email,
                                Username = user.Username

                            };
                            await userService.UpdateAsync(userId, userForUpdate);
                            await Console.Out.WriteLineAsync("Successfully updated");
                            break;
                        }
                    case 3:
                        {
                            await Console.Out.WriteLineAsync("Enter your New Username ");
                            user.Username = Console.ReadLine();

                            var userForUpdate = new UserForUpdateDto()
                            {


                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                DateOfBirth = user.DateOfBirth,
                                Email = user.Email,
                                Username = user.Username

                            };
                            await userService.UpdateAsync(userId, userForUpdate);
                            await Console.Out.WriteLineAsync("Successfully updated");
                            break;
                        }
                    case 4:
                        {
                            await Console.Out.WriteLineAsync("Enter your New Email ");
                            user.Email = Console.ReadLine();

                            var userForUpdate = new UserForUpdateDto()
                            {


                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                DateOfBirth = user.DateOfBirth,
                                Email = user.Email,
                                Username = user.Username

                            };
                            await userService.UpdateAsync(userId, userForUpdate);
                            await Console.Out.WriteLineAsync("Successfully updated");
                            break;
                        }
                    case 5:
                        {
                            await Console.Out.WriteLineAsync("Enter your New Date of Birth ");
                            user.DateOfBirth =DateOnly.Parse( Console.ReadLine());

                            var userForUpdate = new UserForUpdateDto()
                            {


                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                DateOfBirth = user.DateOfBirth,
                                Email = user.Email,
                                Username = user.Username

                            };
                            await userService.UpdateAsync(userId, userForUpdate);
                            await Console.Out.WriteLineAsync("Successfully updated");
                            break;
                        }
                    case 10:
                        {
                            return;
                        }
                }
            }
        }catch (PersonalFinanceException ex)
        {
            await Console.Out.WriteLineAsync("Status code " + ex.StatusCode);
            await Console.Out.WriteLineAsync($"Message: \n {ex.Message}");
        }catch (Exception ex)
        {
            await Console.Out.WriteLineAsync("Message: "+ex.Message);
        }
    }





}
