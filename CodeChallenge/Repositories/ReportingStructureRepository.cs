using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CodeChallenge.Repositories
{
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        private readonly EmployeeContext _employeeContext;

        public ReportingStructureRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public ReportingStructure GetByEmployeeId(String id)
        {
            //Get the employee from the DB. Here I tried to perform a query that did not bring the list
            //of direct reports like in the GetEmployee
            Employee employee = _employeeContext.Employees.SingleOrDefault(i => i.EmployeeId == id);
            //Include the list of direct reports so we can traverse the required lists
            int numberOfReports = GetTotalReports(_employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id));
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = numberOfReports
            };
        }

        public int GetTotalReports(Employee employee)
        {
            //Start count in 0 check if null to avoid unnecessary calls
            int count = 0;
            if (employee.DirectReports == null)
            {
                return count;
            }
            else
            {
                count += employee.DirectReports.Count;
                //Go through the direct reports list of the current employee
                foreach (Employee emp in employee.DirectReports)
                {
                    //Recursion call to traverse the necessary sublists
                    count += GetTotalReports(_employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == emp.EmployeeId));
                }
                return count;
            }
        }
    }
}
