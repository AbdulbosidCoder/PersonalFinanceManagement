using PersonalFinanceManagement.Service.DTOs.User;
using System.Data;

namespace PersonalFinanceManagement.Service.DTOs.Budget;
public class BudgetForResultDto
{
    public int Id { get; set; }
    public UserForResultDto User { get; set; }
    public string Category { get; set; }
    public double Amount { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
