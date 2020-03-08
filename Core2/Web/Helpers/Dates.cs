using System;
using System.Globalization;

namespace Denounces.Web.Helpers
{
    public class Dates
    {
        public static DateTime FormatedDateDo(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return DateTime.Today;
            }

            try
            {
                date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                return Convert.ToDateTime(date);
            }
            catch (Exception)
            {
                date = DateTime.Today.Day.ToString("00") + "/" + DateTime.Today.Month.ToString("00") + "/" + DateTime.Today.Year.ToString();
                return Convert.ToDateTime(date);
            }
        }

        public static DateTime FormatedDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return DateTime.Today;
            }

            try
            {
                var dateTime = new DateTime();
                var dd = DateTime.TryParse(date, out dateTime);
                return Convert.ToDateTime(dateTime);
            }
            catch (Exception)
            {
                //date = DateTime.Today.Day.ToString("00") + "/" + DateTime.Today.Month.ToString("00") + "/" + DateTime.Today.Year.ToString();
                //var dateTime = new DateTime();
                //var dd = DateTime.TryParse(date, out dateTime);
                return DateTime.Today;
            }
        }
    }
}
