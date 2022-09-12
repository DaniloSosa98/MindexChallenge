using CodeChallenge.Models;
using System;

namespace CodeChallenge.Repositories
{
    public interface IReportingStructureRepository
    {
        ReportingStructure GetByEmployeeId(String id);
    }
}
