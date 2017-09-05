using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace AnotherCity.Services
{
    public class FormattingService
    {
        public string AsReadableDate(DateTime? date)
        {
            CultureInfo ci = new CultureInfo("uk-UA");
            if (date != null)
            {
                return  date.Value.ToString("D", ci);
            }
            else
            {
                return "Date not stated yet";
            }
        }
        public string AsReadableTime(DateTime? date)
        {
            if (date != null)
            {
                return date.Value.ToString("HH:mm");
            }
            else
            {
                return "Date not stated yet";
            }
        }
        public string AsDateTime(DateTime? date)
        {
            if (date != null)
            {
                return date.Value.ToString("dd/MM/yyyy hh:mm tt");
            }
            else
            {
                return DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            }
        }
    }
}
