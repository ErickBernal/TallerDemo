using System;
using System.Collections.Generic;

namespace TallerApi.Data.Models;

public partial class EmployeeType
{
    public int IdEmployeeType { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
