// Viona Erika
// 211285T
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using SectionB.Models;
//reference to use section A program and models
using SectionA.Models;

namespace SectionB.Program;

public class program
{
    private static string HR_MASTER_LIST_PATH = "../HRMasterlistB.txt";

    public static async Task Main(string[] args)
    {
        List<Employee> employeeList = SectionA.Program.program.readHRMasterList();

        await Task.Run(() => ProcessPayroll(employeeList));

        PrintEmployeePayroll(employeeList);

        updateMonthlyPayoutToMasterlist(employeeList);
    }

    private static void ProcessPayroll(List<Employee> employeeList)
    {
        foreach (Employee employee in employeeList)
        {
            bool parsed = Enum.TryParse<HireType>(employee.HireType, out HireType hiretype);

            if (!parsed)
            {
                Console.WriteLine("Invalid Hire Type data");
                Environment.Exit(1);
            }

            switch (hiretype)
            {
                case HireType.FullTime:
                    employee.MonthlyPayout = employee.Salary;
                    break;
                case HireType.PartTime:
                    employee.MonthlyPayout = 0.5 * employee.Salary;
                    break;
                case HireType.Hourly:
                    employee.MonthlyPayout = 0.25 * employee.Salary;
                    break;
            }
        }
    }

    private static void PrintEmployeePayroll(List<Employee> employeeList)
    {
        foreach (Employee employee in employeeList)
        {
            Console.WriteLine($"{employee.FullName} ({employee.Nric})");
            Console.WriteLine(employee.Designation);
            Console.WriteLine($"{employee.HireType} Payout: ${employee.MonthlyPayout}");
            Console.WriteLine("------------------------------");
        }
        double? totalPayrollAmount = employeeList.Sum(employee => employee.MonthlyPayout);
        Console.WriteLine(
            $"Total Payroll Amount: ${totalPayrollAmount} to be paid to {employeeList.Count()} employees"
        );
    }

    private static void updateMonthlyPayoutToMasterlist(List<Employee> employeeList)
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter(HR_MASTER_LIST_PATH))
            {
                foreach (Employee employee in employeeList)
                {
                    string[] requiredData =
                    {
                        employee.Nric!,
                        employee.FullName!,
                        employee.Salutation!,
                        employee.StartDate.ToString("dd/MM/yyyy")!,
                        employee.Designation!,
                        employee.Department!,
                        employee.MobileNo!,
                        employee.HireType!,
                        employee.Salary.ToString()!,
                        employee.MonthlyPayout.ToString()!,
                    };
                    outputFile.WriteLine(String.Join("|", requiredData));
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine($"Error trying to write data to {HR_MASTER_LIST_PATH}");
            Environment.Exit(1);
        }
    }
}
