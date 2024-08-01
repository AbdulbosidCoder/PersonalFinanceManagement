using PersonalFinanceManagement.Service.DTOs.User;
using System.Data;

namespace PersonalFinanceManagement.Service.DTOs.Expense;

public class ExpenseForResultDto
{
    public int Id { get; set; }
    public UserForResultDto UserId { get; set; }
    public string Category { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

}
