using PaySlips.Core.Model.Teacher;

namespace PaySlips.Core.Model.PaySlip
{
    public class PaySlipGPX
    {
        public string Number { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime DateCompilation { get; set; }
        public BillingPeriod BillingPeriod { get; set; }
        public TeacherGPX Teacher { get; set; }
    }
}
