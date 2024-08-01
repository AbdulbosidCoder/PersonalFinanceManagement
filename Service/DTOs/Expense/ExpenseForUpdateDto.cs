namespace PersonalFinanceManagement.Service.DTOs.Expense;

public class ExpenseForUpdateDto
{
    public int UserId { get; set; }
    public string Category { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt {  get; set; }

}
