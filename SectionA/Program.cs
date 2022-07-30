// Viona Erika
// 211285T
using System;
using System.Linq;
using System.Collections.Generic;
using SectionA.Models;
using System.Globalization;

namespace SectionA.Program;

public class program
{
    private static string HR_MASTER_LIST_PATH = "../HRMasterlist.txt";
    private static string CORP_ADMIN_INFO_PATH = "../CorporateAdmin.txt";
    private static string IT_DEPARTMENT_INFO_PATH = "../ITDepartment.txt";
    private static string PROCUREMENT_INFO_PATH = "../Procurement.txt";

    private delegate void onboardEmployees(List<Employee> employeeList);

    static void Main(string[] args)
    {
        List<Employee> employeeList = readHRMasterList();

        onboardEmployees onboardEmployees = generateInfoForCorpAdmin;
        onboardEmployees += generateInfoForITDepartment;
        onboardEmployees += generateInfoForProcurement;

        onboardEmployees(employeeList);
    }

    public static List<Employee> readHRMasterList()
    {
        List<Employee> employeeList = new List<Employee>();
        try
        {
            using Stream fileStream = new FileStream(
                HR_MASTER_LIST_PATH,
                FileMode.Open,
                FileAccess.Read
            );
            using (StreamReader file = new StreamReader(fileStream))
            {
                string line;
                while ((line = file.ReadLine()!) != null)
                {
                    string[] employeeListLine = line.Split("|");
                    employeeList.Add(
                        new Employee
                        {
                            Nric = employeeListLine[0],
                            FullName = employeeListLine[1],
                            Salutation = employeeListLine[2],
                            StartDate = DateTime.ParseExact(
                                employeeListLine[3],
                                "dd/MM/yyyy",
                                CultureInfo.InvariantCulture
                            ),
                            Designation = employeeListLine[4],
                            Department = employeeListLine[5],
                            MobileNo = employeeListLine[6],
                            HireType = employeeListLine[7],
                            Salary = Double.Parse(employeeListLine[8])
                        }
                    );
                }
                file.Close();
            }
        }
        catch (Exception)
        {
            Console.WriteLine($"Error trying to read from {HR_MASTER_LIST_PATH}");
            Environment.Exit(1);
        }
        return employeeList;
    }

    private static void generateInfoForCorpAdmin(List<Employee> employeeList)
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter(CORP_ADMIN_INFO_PATH))
            {
                foreach (Employee employee in employeeList)
                {
                    outputFile.WriteLine(employee.generateStringForCorpAdmin());
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine($"Error trying to write to {CORP_ADMIN_INFO_PATH}");
            Environment.Exit(1);
        }
    }

    private static void generateInfoForProcurement(List<Employee> employeeList)
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter(PROCUREMENT_INFO_PATH))
            {
                foreach (Employee employee in employeeList)
                {
                    outputFile.WriteLine(employee.generateStringForProcurement());
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine($"Error trying to write to {PROCUREMENT_INFO_PATH}");
            Environment.Exit(1);
        }
    }

    private static void generateInfoForITDepartment(List<Employee> employeeList)
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter(IT_DEPARTMENT_INFO_PATH))
            {
                foreach (Employee employee in employeeList)
                {
                    outputFile.WriteLine(employee.generateStringForITDepartment());
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine($"Error trying to write to {IT_DEPARTMENT_INFO_PATH}");
            Environment.Exit(1);
        }
    }
}
