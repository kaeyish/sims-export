using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.HCI.View
{

    public class LocationValidationRule : ValidationRule
    {

        private static readonly Regex _regex = new Regex(@"^[\p{L}\p{M}' \.\-]+, [\p{L}\p{M}' \.\-]+$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string strValue))
            {
                return new ValidationResult(false, "Value must be a string.");
            }

            if (!_regex.IsMatch(strValue))
            {
                return new ValidationResult(false, "Invalid format.");
            }

            return ValidationResult.ValidResult;
        }
    }

}
