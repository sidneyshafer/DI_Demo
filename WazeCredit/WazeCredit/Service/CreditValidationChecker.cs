using System;
using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class CreditValidationChecker : IValidationChecker
    {
        public string ErrorMessage => "You did not meet age/salary/credit requirments";

        public bool ValidatorLogic(CreditApplication model)
        {
            if(DateTime.Now.AddYears(-18) < model.DOB)
            {
                return false;
            }
            if(model.Salary < 10000)
            {
                return false;
            }
            return true;
        }
    }
}
