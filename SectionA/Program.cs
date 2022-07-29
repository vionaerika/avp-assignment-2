// Viona Erika
// 211285T
using System;
using System.Linq;
using System.Collections.Generic;
using models;
using System.Globalization;

namespace program;

class program
{
    private static readonly string HR_MASTER_LIST_PATH = "../HRMasterlist.txt";

    static void Main(string[] args)
    {
        Employee employee = new Employee();
        employee.generateListForCorpAdmin();
        List<Employee> employeeList = readHRMasterList();
    }

    public static List<Employee> readHRMasterList()
    {
        List<Employee> employeeList = new List<Employee>();
        using Stream fileStream = new FileStream(
            HR_MASTER_LIST_PATH,
            FileMode.Open,
            FileAccess.Read
        );
        // Type employeeType = typeof(Employee);
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
        return employeeList;
    }
}
