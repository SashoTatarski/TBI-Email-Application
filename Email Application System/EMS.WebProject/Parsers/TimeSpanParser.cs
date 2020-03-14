using EMS.Data;
using EMS.Services.dto_Models;
using System;

namespace EMS.WebProject.Parsers
{
    public static class TimeSpanParser
    {
        public static string StatusParser(EmailDto email)
        {
            var statusChangeInMinutes = (DateTime.UtcNow - email.ToCurrentStatus).Value.Minutes;
            var statusChangeInHours = (DateTime.UtcNow - email.ToCurrentStatus).Value.Hours;
            var statusChangeInDays = (DateTime.UtcNow - email.ToCurrentStatus).Value.Days;

            string currentStatus;

            if (statusChangeInDays <= 0)
            {
                currentStatus = CalculateHours(statusChangeInMinutes, statusChangeInHours);
            }
            else
            {
                currentStatus = statusChangeInDays.ToString() + Constants.TimeParserDays + statusChangeInHours.ToString() + Constants.TimeParserHrsMin + " " + statusChangeInMinutes.ToString() + Constants.TimeParserMin;
            }

            return currentStatus;
        }

        private static string CalculateHours(int statusChangeInMinutes, int statusChangeInHours)
        {
            if (statusChangeInHours <= 0)
            {
                return (statusChangeInMinutes <= 0) ? Constants.TimeParser0Min : statusChangeInMinutes.ToString() + Constants.TimeParserMin;
            }
            else
            {
                return statusChangeInHours.ToString() + Constants.TimeParserHrsMin + statusChangeInMinutes.ToString() + Constants.TimeParserMin;
            }
        }
    }
}
