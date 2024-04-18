using System.ComponentModel.DataAnnotations;
using TrollMarketDataAccess.Models;

namespace TrollMarketWeb.Validations;

public class UniqueUsernameValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value != null){
            var _dbContext = (TrollMarketContext?)validationContext.GetService(typeof(TrollMarketContext))
                ?? throw new NullReferenceException("System Error");

            bool isExist = _dbContext.Accounts.Any(
                    account=>account.Username == value.ToString()
                );
            if(isExist){
                return new ValidationResult($"Username {value.ToString()} is already exist!");
            }
        }
        return ValidationResult.Success;
    }
}
