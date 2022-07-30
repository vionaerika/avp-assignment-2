from datetime import datetime
from functools import *
from Models.Employee import Employee

HR_MASTER_LIST_PATH = "../HRMasterlist.txt"


# function to read employee details from txt file
def read_master_list(file):
    employeeList = []

    # try:
    with open(file, "r") as data:
        for line in data:
            employeeListLine = line.strip().split("|")
            employeeList.append(Employee(
                employeeListLine[0],
                employeeListLine[1],
                employeeListLine[2],
                datetime.strptime(employeeListLine[3], "%d/%m/%Y"),
                employeeListLine[4],
                employeeListLine[5],
                employeeListLine[6],
                employeeListLine[7],
                float(employeeListLine[8])
            ))
    # except:
    #     print(f"Error trying to read from {file}")
    #     exit(1)

    return employeeList


# function to run program
def main():
    employeeList = read_master_list(HR_MASTER_LIST_PATH)
    # Process:
    # 1) filter data to only get FullTime employees that starts after year 1995
    # 2) reduce salary of the employees by 15%
    # 3) add up all the newly calculated salaries
    totalSalary = reduce(lambda a, b: a + b,
                         map(lambda employee: employee.salary * (1 - 0.15),
                             filter(lambda employee: employee.hireType == "FullTime" and employee.startDate.year > 1995, employeeList)))

    print(f"Total of the computed salary: ${totalSalary:.2f}")


# call main function
main()
