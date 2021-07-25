using System;
using System.Collections.Generic;
using System.Linq;
using Rpm.Productie;

namespace Rpm.Misc
{
    public class WeekRange
    {
        public string Name { get; set; }
        public string Range { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Week { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public static List<WeekRange> WeekDays(DateTime startDate, DateTime endDate, bool includethisweek)
        {
            var dateToCheck = startDate;
            var dateRangeBegin = dateToCheck;
            var dateRangeEnd = endDate;
            var weekRangeList = new List<WeekRange>();
            WeekRange weekRange;
            var weeknr = dateRangeBegin.GetWeekNr();
            var year = dateRangeBegin.Year;
            dateRangeBegin = Functions.DateOfWeek(year, DayOfWeek.Monday, weeknr);
            dateRangeEnd = Functions.DateOfWeek(dateRangeEnd.Year, DayOfWeek.Sunday, dateRangeEnd.GetWeekNr());
            var rooster = Manager.Opties?.GetWerkRooster();
            if (rooster != null)
            {
                dateRangeBegin = dateRangeBegin.Add(Manager.Opties.GetWerkRooster().StartWerkdag);
                dateRangeEnd = dateRangeEnd.Add(Manager.Opties.GetWerkRooster().EindWerkdag);
            }

            var xnow = DateTime.Now;
            while (dateRangeBegin < dateRangeEnd)
            {
                var xendDate = Functions.DateOfWeek(year, DayOfWeek.Sunday, weeknr);
                if (rooster != null)
                    xendDate = xendDate.Add(Manager.Opties.GetWerkRooster().EindWerkdag);
                if (!includethisweek && weeknr >= xnow.GetWeekNr()) break;

                weekRange = new WeekRange
                {
                    Name = $"Week {weeknr} {year}",
                    StartDate = dateRangeBegin,
                    EndDate = xendDate,
                    Range = dateRangeBegin.Date.ToShortDateString() + '-' + xendDate.Date.ToShortDateString(),
                    Month = dateRangeBegin.Month,
                    Year = year,
                    Week = weeknr
                };
                if (!weekRangeList.Any(x => x.Week == weekRange.Week && x.Year == weekRange.Year))
                    weekRangeList.Add(weekRange);
                weeknr++;
                if (weeknr > 52)
                {
                    weeknr = 1;
                    year++;
                }

                dateRangeBegin = Functions.DateOfWeek(year, DayOfWeek.Monday, weeknr);
                if (rooster != null)
                    dateRangeBegin = dateRangeBegin.Add(rooster.StartWerkdag);
            }

            return weekRangeList;
        }
    }
}