using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class CreditApprovedLow : ICreditApproved
    {
        public double GetCreditApproved(CreditApplication creditApplication)
        {
            // [complex logix to calculate approval limi] - or hardcode 50% of salary
            return creditApplication.Salary * 0.5;
        }
    }
}
