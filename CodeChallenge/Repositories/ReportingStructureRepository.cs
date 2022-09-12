using CodeChallenge.Data;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            Employee employee = _employeeContext.Employees.SingleOrDefault(i => i.EmployeeId == id);
            int numberOfReports = GetTotalReports(_employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id));
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = numberOfReports
            };
        }

        public int GetTotalReports(Employee employee)
        {
            int count = 0;
            if (employee.DirectReports == null)
            {
                return count;
            }
            else
            {
                count += employee.DirectReports.Count;
                foreach (Employee emp in employee.DirectReports)
                {
                    count += GetTotalReports(_employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == emp.EmployeeId));
                }
                return count;
            }
        }
    }
}
