using EMS.Data;
using EMS.Services.dto_Models;
using System;

namespace EMS.WebProject.Parsers
{
    public static class TimeSpanParser
    {
        public static string StatusParser(EmailDto email)
        {
            var statusChangeMinutes = (DateTime.UtcNow - email.ToCurrentStatus).Value.Minutes;
            var statusChangeHours = (DateTime.UtcNow - email.ToCurrentStatus).Value.Hours;
            var statusChangeDays = (DateTime.UtcNow - email.ToCurrentStatus).Value.Days;

            string currStatusTemp;

            if (statusChangeDays <= 0)
            {
                if (statusChangeHours <= 0)
                {
                    if (statusChangeMinutes <= 0)
                    {
                        currStatusTemp = Constants.TimeParser0Min;
                    }
                    else
                    {
                        currStatusTemp = statusChangeMinutes.ToString() + Constants.TimeParserMin;
                    }
                }
                else
                {
                    currStatusTemp = statusChangeHours.ToString() + Constants.TimeParserHrsMin + statusChangeMinutes.ToString() + Constants.TimeParserMin;
                }
            }
            else
            {
                currStatusTemp = statusChangeDays.ToString() + Constants.TimeParserDays + statusChangeHours.ToString() + Constants.TimeParserHrsMin + " " + statusChangeMinutes.ToString() + Constants.TimeParserMin;
            }

            return currStatusTemp;
        }
    }
}
