using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //sample of date format should come client format: 1 Jan 2018 or 14 Dec 2018
            DateTime datetime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "d MMM yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out datetime);

            return (isValid && datetime > DateTime.Now);
        }
    }
}