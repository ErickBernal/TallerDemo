using System;
using System.Collections.Generic;

namespace TallerApi.Data.Models;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public int? IdEmployeeType { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual EmployeeType? IdEmployeeTypeNavigation { get; set; }
}
