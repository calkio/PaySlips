using PaySlips.Core.Model.Teacher;

namespace PaySlips.Core.Model.PaySlip
{
    public class PaySlipTD
    {
        public string Number { get; set; }
        public DateTime DateSigning { get; set; }
        public BillingPeriod BillingPeriod { get; set; }
        public TeacherTD Teacher { get; set; }
    }
}
