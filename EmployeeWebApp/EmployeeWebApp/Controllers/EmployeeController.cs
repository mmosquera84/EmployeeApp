using EmployeeWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace EmployeeWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> GetEmployee(string EmployeeId)
        {
            string url = "http://masglobaltestapi.azurewebsites.net/api/Employees";

            using (HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);

                List<Employee> employees = null;

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();                  
                    employees = JsonConvert.DeserializeObject<List<Employee>>(data);

                    if (!string.IsNullOrEmpty(EmployeeId))
                    {
                        List<Employee> single = new List<Employee>();
                        var employee = employees.Find(x => x.id == EmployeeId);
                        if (employee != null)
                        {
                            if (employee.contractTypeName == ContractTypeName.HourlySalaryEmployee)
                            {
                                employee.AnnualSalary = 120 * employee.hourlySalary * 12;
                            }
                            else
                            {
                                employee.AnnualSalary = employee.monthlySalary * 12;
                            }

                            single.Add(employee);
                            return View("Index", single);
                        }
                        else
                        {
                            ViewBag.Error = "Employee not found";
                            return View("Index");
                        }
                    }
                    else
                    {
                        foreach (var employee in employees)
                        {
                            if (employee.contractTypeName == ContractTypeName.HourlySalaryEmployee)
                            {
                                employee.AnnualSalary = 120 * employee.hourlySalary * 12;
                            }
                            else
                            {
                                employee.AnnualSalary = employee.monthlySalary * 12;
                            }
                        }
                        return View("Index", employees);
                    }
                }
                else
                {
                    ViewBag.Error = "Bad Request";
                    return View("Index");
                }
            }
        }
    }
}