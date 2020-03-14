using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EMS.WebProject.Models
{
    public class CustomPasswordValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string password = value.ToString();

                return Regex.IsMatch(password, @"^(?=.{1,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\W).*$");
            }

            return false;
        }
    }
}
