using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        //Even if we use the EmployeeId to find the compensations, an ID is needed since they are being saved in the DB
        public String CompensationId { get; set; }
        public Employee Employee { get; set; }
        public int Salary { get; set; }
        public String EffectiveDate { get; set; }
    }
}
