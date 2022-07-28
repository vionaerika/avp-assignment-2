﻿// Viona Erika
// 211285T
using System;
using System.Collections.Generic;
using System.Linq;
using models;

namespace program;

class program
{
    static void Main(string[] args)
    {
        List<List<string>> employeeList = readHRMasterList();
        foreach (List<string> employee in employeeList)
        {
            foreach (string details in employee)
            {
                Console.Write($"{details} ");
            }
            Console.WriteLine();
        }
    }

    public static List<List<string>> readHRMasterList()
    {
        string path = "../HRMasterlist.txt";
        List<List<string>> employeeList = new List<List<string>>();
        using Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        using (StreamReader file = new StreamReader(fileStream))
        {
            string line;

            while ((line = file.ReadLine()!) != null)
            {
                List<string> employeeListLine = line.Split("|").ToList();
                employeeList.Add(employeeListLine);
            }
            file.Close();
        }
        return employeeList;
    }
}
