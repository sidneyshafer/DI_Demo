using WazeCredit.Models;

namespace WazeCredit.Service
{
    public interface IValidationChecker
    {
        bool ValidatorLogic(CreditApplication model);
        string ErrorMessage {  get; }
    }
}
