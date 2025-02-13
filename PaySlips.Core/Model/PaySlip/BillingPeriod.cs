namespace PaySlips.Core.Model.PaySlip
{
    public class BillingPeriod
    {
        public string Number { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Semester Semester { get; set; }
    }
}
