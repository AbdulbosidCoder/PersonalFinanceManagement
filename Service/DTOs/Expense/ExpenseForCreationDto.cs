namespace PersonalFinanceManagement.Service.DTOs.Expense;

public class ExpenseForCreationDto
{
    public int UserId { get; set; }
    public string Category { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }

}
