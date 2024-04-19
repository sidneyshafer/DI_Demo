using WazeCredit.Models;

namespace WazeCredit.Service
{
    public interface ICreditApproved
    {
        double GetCreditApproved(CreditApplication creditApplication);
    }
}
