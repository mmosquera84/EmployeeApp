using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApp.Models
{
    public enum ContractTypeName
    {
        HourlySalaryEmployee = 1,
        MonthlySalaryEmployee = 2
    }

    public class Employee
    {
        public string id { get; set; }
        public string name { get; set; }
        public ContractTypeName contractTypeName { get; set; }
        public string roleId { get; set; }
        public string roleName { get; set; }
        public string roleDescription { get; set; }
        public double hourlySalary { get; set; }
        public double monthlySalary { get; set; }
        public double AnnualSalary { get; set; }
    }
}