using PersonalFinanceManagement.Domain.Commons;

namespace PersonalFinanceManagement.Domain.Entities;

public class Expense : Auditable
{
    public int UserId { get; set; }
    public string Category { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
}
