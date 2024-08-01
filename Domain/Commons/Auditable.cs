using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceManagement.Domain.Commons;

public abstract class Auditable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

}
