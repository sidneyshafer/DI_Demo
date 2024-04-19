using System.Collections.Generic;
using System.Threading.Tasks;
using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class CreditValidator : ICreditValidator
    {
        private readonly IEnumerable<IValidationChecker> _valdations;

        public CreditValidator(IEnumerable<IValidationChecker> valdations)
        {
            _valdations = valdations;
        }

        public async Task<(bool, IEnumerable<string>)> PassAllValidations(CreditApplication model)
        {
            bool validationsPassed = true;
            List<string> errors = new List<string>();

            foreach(var val in _valdations)
            {
                if(!val.ValidatorLogic(model))
                {
                    // Error
                    errors.Add(val.ErrorMessage);
                    validationsPassed = false;
                }
            }

            return (validationsPassed, errors);
        }
    }
}
