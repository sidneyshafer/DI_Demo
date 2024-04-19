using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class CreditApprovedHigh : ICreditApproved
    {
        public double GetCreditApproved(CreditApplication creditApplication)
        {
            // [complex logix to calculate approval limi] - or hardcode 30% of salary
            return creditApplication.Salary * 0.3;
        }
    }
}
