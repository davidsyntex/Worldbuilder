namespace Worldbuilder
{
    internal class Character
    {
        public Character()
        {
            Firstname = "anders";
            Dynasty = "knutson";
            isAlive = true;
            BornYear = 1;
            BornSeason = 1;
            BornDay = 0;
        }

        public Character(int bornYear, int bornSeason, int bornDay)
        {
            Firstname = "anders";
            Dynasty = "knutson";
            isAlive = true;
            BornYear = bornYear;
            BornSeason = bornSeason;
            BornDay = bornDay;
        }

        public Character(int bornYear, int bornSeason, int bornDay, string firstname)
        {
            Firstname = firstname;
            Dynasty = "knutson";
            isAlive = true;
            BornYear = bornYear;
            BornSeason = bornSeason;
            BornDay = bornDay;
        }

        public string Firstname { get; private set; }
        public string Dynasty { get; private set; }
        public bool isAlive { get; private set; }
        public int BornYear { get; private set; }
        public int BornSeason { get; private set; }
        public int BornDay { get; private set; }
        public int DiedYear { get; private set; }
        public int DiedSeason { get; private set; }
        public int DiedDay { get; private set; }

        public void Kill(int diedYear, int diedSeason, int diedDay)
        {
            isAlive = false;
            DiedYear = diedYear;
            DiedSeason = diedSeason;
            DiedDay = diedDay;
        }

        public int Age(int currentYear)
        {
            return currentYear - BornYear;
        }

        public string PrintName()
        {
            return Firstname + " " + Dynasty;
        }

        public bool IsBorn(int currentYear, int currentSeason, int currentDay)
        {
            if (currentYear < BornYear || currentSeason < BornSeason || currentDay < BornDay)
            {
                return false;
            }
            return isAlive;
        }
    }
}