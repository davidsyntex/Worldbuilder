namespace Worldbuilder
{
    public class WorldDate
    {
        public WorldDate(int year, int season, int day)
        {
            Day = day;
            Season = season;
            Year = year;
        }

        public int Year { get; set; }

        public int Season { get; set; }

        public int Day { get; set; }

        public string FormattedDate()
        {
            return HelperFunctions.FormatDate(this);
        }
    }
}