using System;

namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        //Since the data of this model is not going to the DB we don't need an ID/Key
        public Employee Employee { get; set; }
        public int NumberOfReports { get; set; }
    }
}
