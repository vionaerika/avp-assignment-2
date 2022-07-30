// Viona Erika
// 211285T
using System;
using System.Collections.Generic;

namespace models;

public class Employee
{
    public string? Nric { get; set; }
    public string? FullName { get; set; }
    public string? Salutation { get; set; }
    public DateTime StartDate { get; set; }
    public string? Designation { get; set; }
    public string? Department { get; set; }
    public string? MobileNo { get; set; }
    public string? HireType { get; set; }
    public double? Salary { get; set; }
    public double? MonthlyPayout { get; set; } = 0.0;

    public string generateListForCorpAdmin()
    {
        string[] requiredData = { FullName!, Designation!, Department! };
        return (String.Join(",", requiredData));
    }

    public string generateListForProcurement()
    {
        string[] requiredData = { FullName!, Salutation!, Designation!, Department!, MobileNo! };
        return (String.Join(",", requiredData));
    }

    public string generateListForITDepartment()
    {
        string[] requiredData =
        {
            Nric!,
            FullName!,
            StartDate.ToString("dd/MM/yy"),
            Department!,
            MobileNo!
        };
        return (String.Join(",", requiredData));
    }
}
