using CodeChallenge.Models;
using CodeChallenge.Repositories;
using System;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IReportingStructureRepository _reportingStructureRepository;

        public ReportingStructureService(IReportingStructureRepository reportingStructureRepository)
        {
            _reportingStructureRepository = reportingStructureRepository;
        }

        public ReportingStructure GetByEmployeeId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _reportingStructureRepository.GetByEmployeeId(id);
            }

            return null;
        }
    }
}
