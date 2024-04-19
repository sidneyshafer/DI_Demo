using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class AddressValidationChecker : IValidationChecker
    {
        public string ErrorMessage => "Address validation failed";

        public bool ValidatorLogic(CreditApplication model)
        {
            if(model.PostalCode <= 0 || model.PostalCode >= 99999)
            {
                return false;
            }
            return true;
        }
    }
}
